using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TapLearn.Layer.Entites.User;

namespace TapLearn.Core.DTOs
{
    public   class UsersViewModel
    {
        public List<User> users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

    }
    public  class CreateUserViewModel
    {
        [Display(Name = "نام کاربری  ")]
        //[MaxLength(200)]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public string UserName { get; set;}

        [Display(Name = "ایمیل ")]
        //[MaxLength(200)]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید.")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //[MaxLength(200)]
        public string Password { get; set; }
        public IFormFile UserAvator { get; set; }
        //public List<int> SelectedRoles { get; set; }

        
    }
    public class EditUserViewModel
    {
        public int userid { get; set; }
        public string UserName { get; set; }
        [Display(Name = "ایمیل ")]
        //[MaxLength(200)]

        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        //[MaxLength(200)]
        public string Password { get; set; }

        public IFormFile UserAvator { get; set; }
        //نقش هایی که بهش در حالت ویرایش می دهیم 
        public List<int > UserRoles { get; set; }
        //نمایش عکسی که قبلا انتخاب کرده است 
        public string AvatarName { get; set; }
    }
}
