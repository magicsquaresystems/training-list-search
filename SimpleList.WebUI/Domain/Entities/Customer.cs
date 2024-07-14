using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleList.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
