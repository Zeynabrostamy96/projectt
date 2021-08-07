using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TapLearn.Layer.Entites.Order;
using TapLearn.Layer.Entites.Orders;
using TapLearn.Layer.Entites.User;

namespace TapLearn.Layer.Entites.Course
{
    public class Curse
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubGroup { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int SId { get; set; }

        [Required]

        public int Lid { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitel { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CorseDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }

        [MaxLength(600)]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string CourseImageName { get; set; }

        [MaxLength(100)]
        public string DemoFileName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateData { get; set; }


        #region Relations

        [ForeignKey("TeacherId")]
        public User.User user { get; set; }

        [ForeignKey("GroupId")]
        public CourseGroup CourseGroup { get; set; }

        [ForeignKey("SubGroup")]
        public CourseGroup Group { get; set; }
        [ForeignKey("SId")]
        public CourseStatus CourseStatus { get; set; }
        [ForeignKey("Lid")]
        public CourseLevel CourseLevel { get; set; }

        public List<CourseEpisod> courseEpisods { get; set; }
        
        public List<Details> OrderDetails { get; set; }
        public List<UseCourse> UseCourses { get; set; }
       
        public List<Note> notes { get; set; }
        public List<CourseVolte> courseVoltes { get; set; }
         #endregion
    }
}
