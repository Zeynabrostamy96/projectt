using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TapLearn.Layer.Entites.Course
{
    public class CourseVolte
    {
        [Key]
        public int VoteId { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public bool Volte { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.Now;
        #region Relation
        [ForeignKey("userId")]
        public User.User user { get; set; }
        
        public Curse Course { get; set; }
        #endregion
    }
}
