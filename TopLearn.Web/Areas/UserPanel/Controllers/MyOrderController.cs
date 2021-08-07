using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TapLearn.Core.DTOs.order;
using TapLearn.Core.Services.interfaces;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrderController : Controller
    {
       
        private IOrderService _orderService;
        public MyOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View(_orderService.GetUserOrders(User.Identity.Name));
        }
        public IActionResult ShowOrder(int id,bool finaly=false,string type="")
        {
            var order = _orderService.GetOrderForUserPanel(User.Identity.Name, id);
            ViewBag.finaly = finaly;
            ViewBag.discountType = type;
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        public IActionResult FinalyOrder(int id)
        {
            if (_orderService.FinallyOrder(User.Identity.Name,id))
            {
                return Redirect("/UserPanel/MyOrder/ShowOrder/" + id + "?finaly=true");
            }
            return BadRequest();
        }
        public IActionResult UseDisCount(int orderid ,string code)
        {
            DisCountUseType type = _orderService.UsedisCount(orderid, code);


            return Redirect("/UserPanel/MyOrder/ShowOrder/"+orderid+ "?type="+type.ToString());
        }
    }
}
