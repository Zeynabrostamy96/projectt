using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TapLearn.Layer.Entites.Orders;

namespace TapLearn.Layer.Entites.User
{
    public class UserDiscountCode
    {
        [Key]
        public int US_Id { get; set; }
        public int Userid { get; set; }
        public int DisCountId { get; set; }
        //Relition
        
        public User user { get; set; }
        
        public DisCount disCount { get; set; }
    }
}
