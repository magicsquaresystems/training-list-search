using System;
using System.Linq;
using System.Threading.Tasks;
using SimpleList.Domain.Entities;
using SimpleList.WebUI.Models;
using SimpleList.WebUI.Domain.Repository;
using System.Web.Mvc;

namespace SimpleList.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepository<Order> _orderRepository;

        public HomeController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: /
        public async Task<ActionResult> Index(string customerSearch, string productSearch, string searchOperation)
        {
            // Get orders with related entities from repository asynchronously
            var orders = await _orderRepository.GetAllIncludingAsync(o => o.Customer, o => o.Product);

            // Apply filters based on the search operation
            if (!string.IsNullOrEmpty(customerSearch) || !string.IsNullOrEmpty(productSearch))
            {
                if (searchOperation == "AND")
                {
                    if (!string.IsNullOrEmpty(customerSearch))
                    {
                        orders = orders.Where(o => o.Customer.Name.IndexOf(customerSearch, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    if (!string.IsNullOrEmpty(productSearch))
                    {
                        orders = orders.Where(o =>
                            o.Product.ProductCode.IndexOf(productSearch, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                }
                else // OR operation
                {
                    orders = orders.Where(o =>
                        (!string.IsNullOrEmpty(customerSearch) && o.Customer.Name.IndexOf(customerSearch, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrEmpty(productSearch) && o.Product.ProductCode.IndexOf(productSearch, StringComparison.OrdinalIgnoreCase) >= 0));
                }
            }

            // Map domain entities to view models asynchronously
            var orderViewModels = orders.Select(order => new OrderViewModel
            {
                OrderId = order.OrderId,
                CustomerName = order.Customer?.Name ?? "Unknown",
                ProductBrand = order.Product?.Brand ?? "Unknown",
                ProductDescription = order.Product?.Description ?? "Unknown",
                ProductCost = order.Product != null ? order.Product.Cost : -1,
                ProductCode = order.ProductCode ?? "Unknown",
            }).ToList();

            // Pass search parameters to the view to maintain state
            ViewBag.CustomerSearch = customerSearch;
            ViewBag.ProductSearch = productSearch;
            ViewBag.SearchOperation = searchOperation ?? "AND"; // Default to AND if not specified

            return View(orderViewModels);
        }
    }
}
