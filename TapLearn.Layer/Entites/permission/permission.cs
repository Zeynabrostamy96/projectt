using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TapLearn.Layer.Entites.permission
{
   public class permission
   {
        [Key]
        public  int PermissionId { get; set; }
        [Display(Name = "عنوان نقش")]
        [MaxLength(200, ErrorMessage = "{بیشتراز {1}نمیتوانیدکاراکتروارد کنید { 0")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید.")]
        public string PermissionTitle { get; set; }
        public int? ParentId { get; set; }

        #region Relation
        public List<RolePermission> rolePermissions { get; set; }
        [ForeignKey("ParentId")]
        public List<permission> permissions { get; set; }

        #endregion
    }
}
