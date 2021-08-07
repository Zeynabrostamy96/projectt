using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TapLearn.Core.Services.interfaces;
using TapLearn.Layer.Entites.Orders;

namespace TopLearn.Web.Pages.Discount
{
    public class CreateDiscountModel : PageModel
    {
        private IOrderService _orderService;
        public CreateDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;

        }
       [BindProperty]
        public DisCount disCount { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost(string StDate="",string EdDate="")
        {
            if (StDate != "")
            {
                string[] std = StDate.Split('/');
                disCount.StartDate= new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                    );
            }
            if (EdDate != null)
            {
                string[]Ed = EdDate.Split("/");
                disCount.EndDate= new DateTime
                    (
                    int.Parse(Ed[0]),
                    int.Parse(Ed[1]),
                    int.Parse(Ed[2]),
                     new PersianCalendar()
                    );

            }
         
            if (!ModelState.IsValid && _orderService.IsExistCode(disCount.DiscountCode))
                return Page();
            _orderService.AddDiscount(disCount);
            return RedirectToPage("Index");
        }
        public IActionResult OnGetCheckCode(string code)
        {
          return Content(_orderService.IsExistCode(code).ToString()) ;
        }
    }
}
