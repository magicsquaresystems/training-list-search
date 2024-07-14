using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SimpleList.Domain.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public string ProductCode { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
