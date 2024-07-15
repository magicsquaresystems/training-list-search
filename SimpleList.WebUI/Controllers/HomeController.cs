using DocumentFormat.OpenXml.Spreadsheet;
using SimpleList.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SimpleList.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /
        public HomeController()
        {

        }

        // GET: Customers
        public ActionResult Index(OrderSearchModel search)
        {

            var orders = db.Orders
              .OrderBy(o => o.OrderDate)
              .ToList();

            List<SelectListItem> SearchByList = new List<SelectListItem>
            {
                new SelectListItem  {Text = "Customer",Value = "Customer" },
                new SelectListItem  {Text = "Product",Value = "Product" },
            };

            search.SearchByList = SearchByList;

            if (!string.IsNullOrEmpty(search.Name) && string.IsNullOrEmpty(search.SearchByValue))
            {
                orders = db.Orders.Where(x => x.User.FirstName.Contains(search.Name) || x.User.LastName.Contains(search.Name)
                || x.Product.Code.Contains(search.Name) || x.Product.Description.Contains(search.Name) || x.Product.Brand.Contains(search.Name))
                  .OrderBy(o => o.OrderDate)
                  .ToList();
            }
            else
            {
                if (!string.IsNullOrEmpty(search.SearchByValue) && !string.IsNullOrEmpty(search.Name))
                {
                    if (search.SearchByValue.Equals("Customer"))
                    {
                        orders = db.Orders.Where(x => x.User.FirstName.Contains(search.Name) || x.User.LastName.Contains(search.Name))
      .OrderBy(o => o.OrderDate)
      .ToList();

                    }
                   
                    if (search.SearchByValue.Equals("Product") && !string.IsNullOrEmpty(search.Name))
                    {
                        orders = db.Orders.Where(x => x.Product.Code.Contains(search.Name) || x.Product.Description.Contains(search.Name) || x.Product.Brand.Contains(search.Name))
                     .OrderBy(o => o.OrderDate)
                     .ToList();
                    }
                }               
            }

            var model = new OrderListModel
            {
                Orders = orders,
                Search = search

            };

            SetNavOption(NavOption.Examples);
            return View(model);
        }

    }

}