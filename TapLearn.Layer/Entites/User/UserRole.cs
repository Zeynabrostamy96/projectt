using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TapLearn.Layer.Entites.User;

namespace TapLearn.Layer.Entites.User
{
    public class UserRole
    {
        public UserRole()
        {

        }
        [Key]
        public int UR_Id {get;set;}
        public int Roleid { get; set; }
        public int Userid { get; set; }

        #region Relation
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        #endregion

    }
}
