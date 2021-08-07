using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
//using TapLearn.Core.Course;

namespace TapLearn.Layer.Entites.Order
{
    public class Details
    {
        [Key]
        public int DetailsId { get; set; }
        public int OId { get; set; }
        public int Courseid { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        //Relition
        [ForeignKey("OId")]
        public Order Order { get; set; }
        [ForeignKey("Courseid")]
        public Course.Curse Curse { get; set; }


    }
}
