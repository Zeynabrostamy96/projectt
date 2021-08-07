using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
//using TapLearn.Core.Course;
//using TapLearn.Layer.Entites.CoursGroup;
using TapLearn.Layer.Entites.Order;
using TapLearn.Layer.Entites.Course;
using TapLearn.Layer.Entites.Orders;

namespace TapLearn.Layer.Entites.User
{
   public class User
   {
        public User()
        {

        }

        [Key]
        public int Userid { get; set; }

        [Display(Name = "نام کاربر")]
        [MaxLength(200, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        [Required(ErrorMessage ="لطفا{0}را وارد کنید.")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        //[EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد.")]
        public string Email { get; set; }

        [Display(Name = "نام کاربر")]
        [MaxLength(50, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        public string ActiveCode { get; set; }

        [Display(Name = "وضعیت")]

        public bool IsActive { get; set; }

        [Display(Name = "اواتار")]
        [MaxLength(200, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام ")]
        [MaxLength(200, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }
        public bool IsDelete { get; set; }

        #region Relaitian
        public virtual List<UserRole> userRoles { get; set; }
        public virtual List<Wallet.wallet> Wallets { get; set; }
        public virtual List<Curse> Courses { get; set; }
        public virtual List<Order.Order> Orders { get; set; }
        public virtual List<UseCourse> UseCourses { get; set; }
        public virtual List<UserDiscountCode> UserDiscountCodes { get; set; }
        //public virtual List<CourseNote> Comments { get; set; }
      
        public List<Note> notes { get; set; }
        public List<CourseVolte > courseVoltes { get; set; }
        #endregion
    }
}
