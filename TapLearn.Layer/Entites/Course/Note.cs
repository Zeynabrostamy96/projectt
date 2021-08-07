using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TapLearn.Layer.Entites.Course;

namespace TapLearn.Layer.Entites.Course
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        public int CourseId { get; set; }
      
        public int Userid { get; set; }
        [MaxLength(700)]
        public string Comment { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminRead { get; set; }
        public DateTime CreateDate { get; set; }
        //Relation
      
        public virtual Curse curse { get; set; }

       
        public virtual User.User user { get; set; }
    }
}
