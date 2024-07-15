using ClosedXML.Excel;
using SimpleList.Domain.Concrete;
using SimpleList.WebUI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SimpleList.WebUI.Controllers
{
    public class BaseController : Controller
    {
        // All shared objects go here
        protected ApplicationDbContext db;

        public BaseController()
        {
            db = new ApplicationDbContext();
           // ReadExcelAndInsertData();
        }

        public void ReadExcelAndInsertData()
        {

            bool IsDataReadFromExcelFile = Convert.ToBoolean(ConfigurationManager.AppSettings["IsDataReadFromExcelFile"]);

            string filePath = HttpRuntime.AppDomainAppPath.Replace("\\SimpleList.WebUI", "") + "Data" + "\\Orders.xlsx";

            if (!IsDataReadFromExcelFile)
            {
                if (System.IO.File.Exists(filePath))
                {

                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet1 = workbook.Worksheet("Customer orders");


                        using (var context = new ApplicationDbContext())
                        {
                            foreach (var row in worksheet1.RowsUsed().Skip(1)) // Skip header row
                            {

                                // add customer 
                                var user = new User
                                {
                                    FirstName = row.Cell(2).GetValue<string>().Split(' ')[0],
                                    LastName = row.Cell(2).GetValue<string>().Split(' ')[1]
                                };
                                context.Users.Add(user);

                                // add product 
                                var product = new Product
                                {
                                    Code = row.Cell(3).GetValue<string>(),
                                    Brand = row.Cell(4).GetValue<string>(),
                                    Description = row.Cell(5).GetValue<string>(),
                                    Cost = row.Cell(6).GetValue<double>()
                                };
                                context.Products.Add(product);

                                // add order
                                Order order = new Order();
                                order.ProductID = product.ProductID;
                                order.UserId = user.UserId;
                                order.OrderDate = DateTime.UtcNow;
                                context.Orders.Add(order);
                                context.SaveChanges();

                            }

                        }
                    }

                    ConfigurationManager.AppSettings["IsDataReadFromExcelFile"] = "true";

                }
            }

        }

        protected void SetNavOption(NavOption navId)
        {
            ViewBag.NavId = navId;
        }
    }
}