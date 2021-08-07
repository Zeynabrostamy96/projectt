using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TapLearn.Layer.Entites.User;

namespace TapLearn.Layer.Entites.Orders
{
    public  class DisCount
    {
       [Key]
       public int DisCountId { get; set; }
        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
     
        public string DiscountCode { get; set; }
        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public int DiscountPercent { get; set; }
       public int? UsableCount { get; set; }
       public DateTime? StartDate { get; set; }
       public DateTime? EndDate { get; set; }
        //Relation
        public List<UserDiscountCode> discountCode { get; set; }

    }
}
