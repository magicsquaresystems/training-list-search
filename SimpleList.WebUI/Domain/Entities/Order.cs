using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleList.WebUI.Domain.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

       // [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        //[ForeignKey("ProductID")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public DateTime OrderDate { get; set; }

    }
}