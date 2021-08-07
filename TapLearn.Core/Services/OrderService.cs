using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TapLearn.Core.DTOs.order;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Context;
using TapLearn.Layer.Entites.Order;


namespace TapLearn.Core.Services
{
    public class OrderService : IOrderService
    {
        private TopLearnContext _context;
        
        private IUserService _userService;
        public OrderService(IUserService userService, TopLearnContext contex)
        {
            _userService = userService;
            _context = contex;
        }

        public void AddDiscount(Layer.Entites.Orders.DisCount disCount)
        {
            _context.disCounts.Add(disCount);
            _context.SaveChanges();
        }

        public int AddOrder(string username, int Courseid)
        {
            int userid = _userService.GetUserIdByUserName(username);
            Order order = _context.orders.FirstOrDefault(o => o.Uid == userid && !o.IsFainally);
            var Course = _context.curses.Find(Courseid);
            if (order == null)
            {
                order = new Order()
                {
                    Uid = userid,
                    IsFainally = false,
                    CreateDate = DateTime.Now,
                    OrderSum = Course.CoursePrice,
                    Details = new List<Details>()
                    {
                        new Details()
                        {
                            Courseid=Courseid,
                            Count=1,
                            Price=Course.CoursePrice
                        }

                    }
                };
                _context.orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                Details details = _context.detail
                    .FirstOrDefault(o => o.OId == order.OrderId && o.Courseid == Courseid);
                if (details != null)
                {
                    details.Count += 1;
                    _context.detail.Update(details);
                }
                else
                {
                    details = new Details()
                    {
                        OId = order.OrderId,
                        Count = 1,
                        Courseid = Courseid,
                        Price = Course.CoursePrice
                    };
                    _context.detail.Add(details);

                }
                _context.SaveChanges();
                UpdatePriceOrder(order.OrderId);
            }
            return order.OrderId;
        }

        public bool FinallyOrder(string username, int orderid)
        {
            int userid = _userService.GetUserIdByUserName(username);
            var order = _context.orders.Include(o => o.Details).ThenInclude(od => od.Curse)
                .SingleOrDefault(o => o.OrderId == orderid && o.Uid == userid);
            if(order==null || order.IsFainally)
            {
                return false;
            }
            if(_userService.AccountBalance(username)>= order.OrderSum)
            {
                order.IsFainally = true;
                //اینجا ماباید موجودی کیف پول را کم کنیم 
                _userService.AddWallet(new Layer.Entites.Wallet.wallet
                {
                    Amount = order.OrderSum,
                    CreateDate = DateTime.Now,
                    IsPay = true,
                    Description=" فاکتورشماره #"+order.OrderId,
                    Userid=userid,
                    TypeId=2

                }) ;
                _context.orders.Update(order);
                foreach (var details in order.Details)
                {
                    _context.useCourses.Add(new Layer.Entites.User.UseCourse()
                    {
                        courseid=details.Courseid,
                        userid=userid,
                    });
                }
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        public Layer.Entites.Orders.DisCount GetDisCountByID(int discountId)
        {
            return _context.disCounts.Find(discountId);
        }

        public List<Layer.Entites.Orders.DisCount> GetDisCounts()
        {
            return _context.disCounts.ToList();
        }

        public Order GetOrderById(int orderid)
        {
            return _context.orders.Find(orderid);
        }

        public Order GetOrderForUserPanel(string username, int orderid)
        {
            int userId = _userService.GetUserIdByUserName(username);

            return _context.orders.Include(o => o.Details).ThenInclude(od => od.Curse)
                .FirstOrDefault(o => o.Uid == userId && o.OrderId == orderid);
           
        }

        public List<Order> GetUserOrders(string userName)
        {
            int userid = _userService.GetUserIdByUserName(userName);
            return _context.orders.Where(o => o.Uid == userid).ToList();
        }

        public bool IsExistCode(string code)
        {
           return  _context.disCounts.Any(c => c.DiscountCode == code);
        }

        public bool IsUserInCourse(string username, int courseid)
        {
            int userid = _userService.GetUserIdByUserName(username);
            return _context.useCourses.Any(u => u.userid == userid && u.courseid == courseid);
        }

        public void UpdateDiscount(Layer.Entites.Orders.DisCount disCount)
        {
            _context.disCounts.Update(disCount);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.orders.Update(order);
            _context.SaveChanges();
        }

        public void UpdatePriceOrder(int orderid)
        {
            Order order = _context.orders.Find(orderid);
            order.OrderSum = _context.detail
                .Where(d => d.OId == order.OrderId)
                .Sum(d => d.Price);
            _context.orders.Update(order);
            _context.SaveChanges();
        }

        public DisCountUseType UsedisCount(int orderid, string code)
        {
            var discount = _context.disCounts.Where(d => d.DiscountCode == code).SingleOrDefault();
            if (discount == null)
                return DisCountUseType.NotFound;

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
                return DisCountUseType.ExpierData;

            if (discount.EndDate != null && discount.EndDate > DateTime.Now)
                return DisCountUseType.ExpierData;

            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DisCountUseType.Finished;
            var order = GetOrderById(orderid);

            if (_context.userDiscountCodes.Any(u => u.Userid == order.Uid && u.DisCountId == discount.DisCountId))
                return DisCountUseType.UserUsed;

            int percent = order.OrderSum * discount.DiscountPercent / 100;
            order.OrderSum = order.OrderSum - percent;
            UpdateOrder(order);
            if(discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }
            _context.disCounts.Update(discount);
            _context.userDiscountCodes.Add(new Layer.Entites.User.UserDiscountCode
            {
                Userid = order.Uid,
                DisCountId=discount.DisCountId

            }) ;
            _context.SaveChanges();
            return DisCountUseType.Success;
        }
        


    }
}
