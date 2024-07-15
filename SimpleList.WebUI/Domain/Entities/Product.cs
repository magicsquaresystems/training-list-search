using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleList.WebUI.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}