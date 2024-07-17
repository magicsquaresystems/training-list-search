using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using ExcelDataReader;
using SimpleList.Domain.Concrete;
using SimpleList.Domain.Entities;

namespace SimpleList.WebUI.Services
{
    public class ExcelDataService
    {
        private readonly ApplicationDbContext _dbContext;

        public ExcelDataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ImportDataFromExcel(string filePath)
        {
            try
            {
                Console.WriteLine("Starting data import...");

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Specify encoding explicitly
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var encoding = Encoding.GetEncoding("windows-1252");

                    using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
                    {
                        FallbackEncoding = encoding
                    }))
                    {
                        // Begin transaction
                        using (var transaction = _dbContext.Database.BeginTransaction())
                        {

                            try
                            {
                                // Enable IDENTITY_INSERT for explicit assignment of Ids
                                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orders ON");

                                // Skip header row
                                reader.Read();

                                // read rows
                                while (reader.Read())
                                {
                                    try
                                    {
                                        // Read customer name
                                        string customerName = reader.GetString(1);

                                        // Create new customer if not already existing
                                        Customer customer = _dbContext.Customers.FirstOrDefault(c => c.Name == customerName);

                                        if (customer == null)
                                        {
                                            Console.WriteLine("Adding new customer");
                                            customer = new Customer { Name = customerName };
                                            _dbContext.Customers.Add(customer);
                                            _dbContext.SaveChanges(); // Save immediately to get the ID
                                        }

                                        // Create new User if not already existing
                                        var user = _dbContext.Users.FirstOrDefault(u => u.FirstName + " " + u.LastName == customerName);
                                        if (user == null)
                                        {
                                            Console.WriteLine("Adding new user");
                                            var names = customerName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                            string firstName = names.Length > 0 ? names[0] : string.Empty;
                                            string lastName = names.Length > 1 ? string.Join(" ", names.Skip(1)) : string.Empty;

                                            user = new ApplicationUser
                                            {
                                                FirstName = firstName,
                                                LastName = lastName
                                            };
                                            _dbContext.Users.Add(user);
                                            _dbContext.SaveChanges();
                                        }

                                        // Save the user id against the cutomer record
                                        Console.WriteLine($"Setting userId for customer {customerName} to {user.UserId}");
                                        customer.UserId = user.UserId;
                                        _dbContext.SaveChanges();

                                        // Read product details
                                        string productCode = reader.GetString(2);
                                        Product product = _dbContext.Products.FirstOrDefault(p => p.ProductCode == productCode);

                                        if (product == null)
                                        {
                                            Console.WriteLine($"Adding new product: {productCode}");
                                            product = new Product
                                            {
                                                ProductCode = productCode,
                                                Brand = reader.GetString(3),
                                                Description = reader.GetString(4),
                                                Cost = Convert.ToDecimal(reader.GetValue(5)) // Convert the value to decimal
                                            };
                                            _dbContext.Products.Add(product);
                                            _dbContext.SaveChanges(); // Save immediately to get the ID
                                        }
                                        else
                                        {
                                            // Update existing product details if necessary
                                            bool isUpdated = false;
                                            if (product.Brand != reader.GetString(3))
                                            {
                                                product.Brand = reader.GetString(3);
                                                isUpdated = true;
                                            }
                                            if (product.Description != reader.GetString(4))
                                            {
                                                product.Description = reader.GetString(4);
                                                isUpdated = true;
                                            }
                                            if (product.Cost != Convert.ToDecimal(reader.GetValue(5)))
                                            {
                                                product.Cost = Convert.ToDecimal(reader.GetValue(5));
                                                isUpdated = true;
                                            }

                                            if (isUpdated)
                                            {
                                                Console.WriteLine($"Updating existing product: {product.ProductCode}");
                                                _dbContext.Entry(product).State = EntityState.Modified;
                                                _dbContext.SaveChanges();
                                            }
                                        }

                                        // Read order id
                                        int orderId = Convert.ToInt32(reader.GetValue(0));

                                        // Has order been created
                                        Order order = _dbContext.Orders.Find(orderId);

                                        if (order == null) {
                                            // Create new Order and add to DbContext
                                            order = new Order
                                            {
                                                OrderId = orderId,
                                                CustomerId = customer.CustomerId,
                                                ProductCode = product.ProductCode // Assuming you want to keep product code in order
                                            };

                                            _dbContext.Orders.Add(order);
                                            _dbContext.SaveChanges();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"An error occurred while processing a row: {ex.Message}");
                                        PrintInnerExceptions(ex);
                                        transaction.Rollback();
                                        return;
                                    }
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                // Add necessary error printing and rolling back transaction
                                Console.WriteLine($"An error occurred during the import process: {ex.Message}");
                                PrintInnerExceptions(ex);
                                transaction.Rollback();
                            }
                            finally {
                                // Turn off IDENTITY_INSERT finally
                                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Orders OFF");
                            }

                        }
                    }
                }

                Console.WriteLine("Data import completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                PrintInnerExceptions(ex);
            }
        }

        // Recursively print inner exceptions
        private void PrintInnerExceptions(Exception ex)
        {
            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                Console.WriteLine($"Inner exception: {innerException.Message}");
                innerException = innerException.InnerException;
            }
        }
    }
}
