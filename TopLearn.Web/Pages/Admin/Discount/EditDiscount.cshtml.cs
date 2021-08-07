using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.Orders;

namespace TopLearn.Web.Pages.Admin.Discount
{
    public class EditDiscountModel : PageModel
    {
        private IOrderService _orderService;
       
        public EditDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [BindProperty]
        public DisCount disCount { get; set; }
        public void OnGet(int id)
        {
            disCount = _orderService.GetDisCountByID(id);
        }
        public IActionResult OnPost(int id, string StDate = "", string EdDate = "")
        {
            if (StDate != "")
            {
                string[] std = StDate.Split('/');
                disCount.StartDate = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );
            }
            if (EdDate != null)
            {
                string[] Ed = EdDate.Split("/");
                disCount.EndDate = new DateTime
                    (
                    int.Parse(Ed[0]),
                    int.Parse(Ed[1]),
                    int.Parse(Ed[2]),
                     new PersianCalendar()
                    );

            }
            _orderService.UpdateDiscount(disCount);
            return RedirectToPage("Index");
        }
    }
}
