using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TapLearn.Core.DTOs
{
    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        //[MaxLength(200, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
