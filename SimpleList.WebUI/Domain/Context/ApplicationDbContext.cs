using SimpleList.Domain.Entities;
using System.Data.Entity;

namespace SimpleList.Domain.Concrete
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }


        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
        }

        
    }
}
