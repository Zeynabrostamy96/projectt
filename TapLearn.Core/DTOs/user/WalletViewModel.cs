using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TapLearn.Core.DTOs
{
   public class ChargeWalletViewModel
   {
        [Display(Name ="مبلغ ")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public int Amount { get; set; }

   }
    public class walletViewModel
    {
        public int Amount { get; set; }
        public int Type { get; set; }
        public DateTime dateTime { get; set; }
        public  string Description { get; set; }
    }
   
}
