using SimpleList.Domain.Concrete;
using SimpleList.Domain.Entities;
using SimpleList.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SimpleList.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /
        public ActionResult Index(string customerName, string productCode)
        {
            ViewBag.CustomerName = customerName;
            ViewBag.ProductCode = productCode;

            var ordersQuery = db.Orders
                .Include(o => o.User)
                .Include(o => o.Product)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    CustomerName = o.User.FirstName + " " + o.User.LastName,
                    ProductCode = o.Product.ProductCode,
                    Brand = o.Product.Brand,
                    ProductDescription = o.Product.ProductDescription,
                    Cost = o.Product.Cost
                });

            if (!string.IsNullOrEmpty(customerName))
            {
                ordersQuery = ordersQuery.Where(o => o.CustomerName.Contains(customerName));
            }

            if (!string.IsNullOrEmpty(productCode))
            {
                ordersQuery = ordersQuery.Where(o => o.ProductCode.Contains(productCode));
            }

            var orders = ordersQuery.ToList();

            return View(orders);
        }
    }
}