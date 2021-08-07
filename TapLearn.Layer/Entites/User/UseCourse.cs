using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
//using TapLearn.Core.Course;

namespace TapLearn.Layer.Entites.User
{
    public class UseCourse
    {
        [Key]
        public int UC_id { get; set; }

        public int userid { get; set; }

        public int courseid { get; set; }

        //navigation
        public  User  user{ get; set; }

        public Course.Curse curse { get; set; }



    }
}
