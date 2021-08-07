using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TapLearn.Core.DTOs
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربر")]
        //[MaxLength(150)]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل ")]
        //[MaxLength(150)]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(150)]
        public string Password { get; set; }


        [Display(Name = "تکرارکلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(150)]
        [Compare("Password", ErrorMessage = "کلمه عبورمغایرت نمی کند ")]
        public string RePassword { get; set; }


    }
    public class ForgotPasswordViewModel
    {

        [Display(Name = "ایمیل ")]
        //[MaxLength(150)]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }
    }
    public class RestartPasswordViewModel
    {
        public string ActiveCode { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(150)]
        public string Password { get; set; }


        [Display(Name = "تکرارکلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(200)]
        [Compare("Password", ErrorMessage = "کلمه عبورمغایرت نمی کند ")]
        public string RePassword { get; set; }

    }
}
