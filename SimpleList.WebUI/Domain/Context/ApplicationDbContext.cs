using SimpleList.Domain.Entities;
using System.Data.Entity;

namespace SimpleList.Domain.Concrete
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
        }
    }
}
