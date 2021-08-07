using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TapLearn.Layer.Entites.Wallet
{
    public   class WalletType
    {
        public WalletType()
        {
             
        }
      
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }
        [MaxLength(150, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public string TypeTitel { get; set; }
        public virtual List<wallet> Wallet { get; set; }
    }
}
