using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.Orders;

namespace TopLearn.Web.Pages.Discount
{
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;
        
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public List<DisCount> disCount { get; set; }
        public void OnGet()
        {
            disCount = _orderService.GetDisCounts();
        }
    }
}
