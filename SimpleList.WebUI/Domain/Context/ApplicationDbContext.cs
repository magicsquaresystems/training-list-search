using SimpleList.WebUI.Domain.Entities;
using System.Data.Entity;

namespace SimpleList.Domain.Concrete
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>()
            //  .HasKey(c => c.UserId)
            //  .HasMany(c => c.Orders)
            //  .WithRequired(o => o.User)
            //  .HasForeignKey(o => o.UserId);

          

            //modelBuilder.Entity<Order>()
            //    .HasKey(o => o.OrderID);

        }
    }
}
