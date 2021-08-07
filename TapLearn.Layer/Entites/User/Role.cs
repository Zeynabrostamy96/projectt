using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TapLearn.Layer.Entites.permission;

namespace TapLearn.Layer.Entites.User
{
   public class Role
   {
        public Role()
        {

        }

        [Key]
        public int Roleid { get; set; }

        [Display(Name ="عنوان نقش")]
        [Required(ErrorMessage ="لطفا{0}را وارد کنید.")]
        [MaxLength(200,ErrorMessage ="{0}نمیتواند بیشتراز {1}کاراکتر وارد کنید.")]
        public string RoleTitel { get; set; }
        public bool IsDelete { get; set; }
        #region Relatioan
        public virtual List<UserRole> UserRoles { get; set; }
        public List<RolePermission> rolepermissions { get; set; }
        
        #endregion
    }
}
