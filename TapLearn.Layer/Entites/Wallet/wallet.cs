using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TapLearn.Layer.Entites.User;

namespace TapLearn.Layer.Entites.Wallet
{
    public  class wallet
    {
        public wallet()
        {

        }
        [Key]
        public int walletid { get; set; }
        [Display(Name ="نوع تراکنش")]
   
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public int TypeId { get; set; }
        [Display(Name = "کاربر ")]
      
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public int Userid { get; set; }
        [Display(Name = "مبلغ")]

        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public int Amount { get; set; }
        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public string Description { get; set; }
        [Display(Name = "تاییدشده")]
        public bool IsPay { get; set; }
        [Display(Name = "تاریخ وساعت ")]
        public DateTime CreateDate { get; set; }
        //navigation
        [ForeignKey(" TypeId")]
        public virtual WalletType WalletType { get; set; }
        public virtual User.User users { get; set; }

    }
}
