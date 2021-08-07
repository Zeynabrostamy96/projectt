using System;
using System.Collections.Generic;
using System.Text;
using TapLearn.Core.DTOs.order;
using TapLearn.Layer.Entites.Order;
using TapLearn.Layer.Entites.Orders;

namespace TapLearn.Core.Services.interfaces
{
    public  interface IOrderService
    {
        int AddOrder(string username, int Courseid);
        Order GetOrderById(int orderid);
        void UpdatePriceOrder(int orderid);
        Order GetOrderForUserPanel(string userame, int orderid);
        bool FinallyOrder(string username, int orderid);
        List<Order> GetUserOrders(string userName);
        
        DisCountUseType UsedisCount(int orderid, string code);
        void UpdateOrder(Order order);
        void AddDiscount(DisCount disCount);
        List<DisCount> GetDisCounts();
        DisCount GetDisCountByID(int discountId);
        void UpdateDiscount(DisCount disCount);
        bool IsExistCode(string code);
        bool IsUserInCourse(string username, int courseid);

    }
}
