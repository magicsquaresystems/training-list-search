using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SimpleList.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [StringLength(50)]
        public string ProductCode { get; set; }

        [StringLength(50)]
        public string ProductDescription { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        [StringLength(51)]
        public string Brand { get; set; }
    }
}