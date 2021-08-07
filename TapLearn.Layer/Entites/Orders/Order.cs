using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TapLearn.Layer.Entites.Order
{
    public  class Order
    {
        public Order()
        {
        }
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int Uid { get; set; }
        public bool IsFainally { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public int OrderSum { get; set; }

        //Relation
        [ForeignKey("Uid")]
        public virtual User.User user { get; set; }
        public List<Details> Details { get; set; }
    }
}
