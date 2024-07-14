using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleList.Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [ForeignKey(nameof(Product))]
        public string ProductCode { get; set; }

        public Product Product { get; set; }
    }
}
