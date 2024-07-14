using System.ComponentModel.DataAnnotations;

namespace SimpleList.Domain.Entities
{
    public class Product
    {
        [Key]
        public string ProductCode { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
