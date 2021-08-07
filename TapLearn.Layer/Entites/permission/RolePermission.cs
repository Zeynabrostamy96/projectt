using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TapLearn.Layer.Entites.User;

namespace TapLearn.Layer.Entites.permission
{
   public   class RolePermission
   {
        [Key]
        public  int RP_ID { get; set; }
        public int Roleid { get; set; }
        public int PermissionId { get; set; }
        #region relation
        public  Role rolo { get; set; }
        public permission permission { get; set; }
        #endregion



    }
}
