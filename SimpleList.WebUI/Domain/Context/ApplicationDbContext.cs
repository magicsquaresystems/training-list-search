using SimpleList.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SimpleList.Domain.Concrete
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasRequired(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductCode);

            base.OnModelCreating(modelBuilder);
        }

        public static void Seed(ApplicationDbContext context)
        {
            //var users = new List<ApplicationUser>
            //{
            //    new ApplicationUser { FirstName = "Hazel", LastName = "Nutt" },
            //    new ApplicationUser { FirstName = "Clark", LastName = "Kent" },
            //    new ApplicationUser { FirstName = "Barb", LastName = "Akew" },
            //    new ApplicationUser { FirstName = "Isabelle", LastName = "Ringing" },
            //    new ApplicationUser { FirstName = "Chris", LastName = "Anthemum" },
            //    new ApplicationUser { FirstName = "Eileen", LastName = "Sideways" },
            //    new ApplicationUser { FirstName = "Paige", LastName = "Turner" },
            //    new ApplicationUser { FirstName = "Bess", LastName = "Twishes" }
            //};

            //users.ForEach(u => context.Users.Add(u));
            //context.SaveChanges();

            //var products = new List<Product>
            //{
            //    new Product { ProductCode = "L258MT", Brand = "Lightning", ProductDescription = "Running Trainers", Cost = 99.99M },
            //    new Product { ProductCode = "C654KJ", Brand = "Initech", ProductDescription = "Tennis balls", Cost = 4.99M },
            //    new Product { ProductCode = "A617YV", Brand = "Hooli", ProductDescription = "Grey T-shirt", Cost = 24.99M },
            //    new Product { ProductCode = "B651EW", Brand = "Soylent", ProductDescription = "Blue hoodie", Cost = 37.95M },
            //    new Product { ProductCode = "T783ED", Brand = "Lightning", ProductDescription = "Cycling shorts", Cost = 21.99M },
            //    new Product { ProductCode = "R187UH", Brand = "Globex", ProductDescription = "Neon yellow soccer ball", Cost = 12.99M },
            //    new Product { ProductCode = "E176XZ", Brand = "Acme", ProductDescription = "20 x golf tees", Cost = 9.99M }
            //};

            //products.ForEach(p => context.Products.Add(p));
            //context.SaveChanges();

            //var orders = new List<Order>
            //{
            //    new Order { UserId = users.Single(u => u.FirstName == "Hazel" && u.LastName == "Nutt").UserId, ProductCode = "L258MT" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Clark" && u.LastName == "Kent").UserId, ProductCode = "C654KJ" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Barb" && u.LastName == "Akew").UserId, ProductCode = "L258MT" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Isabelle" && u.LastName == "Ringing").UserId, ProductCode = "A617YV" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Hazel" && u.LastName == "Nutt").UserId, ProductCode = "B651EW" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Chris" && u.LastName == "Anthemum").UserId, ProductCode = "A617YV" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Eileen" && u.LastName == "Sideways").UserId, ProductCode = "T783ED" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Clark" && u.LastName == "Kent").UserId, ProductCode = "T783ED" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Paige" && u.LastName == "Turner").UserId, ProductCode = "A617YV" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Barb" && u.LastName == "Akew").UserId, ProductCode = "B651EW" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Chris" && u.LastName == "Anthemum").UserId, ProductCode = "L258MT" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Eileen" && u.LastName == "Sideways").UserId, ProductCode = "C654KJ" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Bess" && u.LastName == "Twishes").UserId, ProductCode = "R187UH" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Isabelle" && u.LastName == "Ringing").UserId, ProductCode = "E176XZ" },
            //    new Order { UserId = users.Single(u => u.FirstName == "Paige" && u.LastName == "Turner").UserId, ProductCode = "T783ED" }
            //};

            //orders.ForEach(o => context.Orders.Add(o));
            //context.SaveChanges();
        }
    }
}
