using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TapLearn.Core.DTOs
{
    public class UserPanelViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Wallet { get; set; }

    }
    public class SideBarUserPanelViewModel
    {
        public string UserName { get; set; }
        public string ImageName { get; set; }
        public DateTime DataRegister { get; set; }

    }
    public class EditProfileViewModel
    {
        [Display(Name = "نام کاربر")]
        //[MaxLength(200)]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل ")]
        //[MaxLength(200)]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }
        public string AvatorName { get; set; }
        public IFormFile UserAvator { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبورفعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string OldPassword { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
}
