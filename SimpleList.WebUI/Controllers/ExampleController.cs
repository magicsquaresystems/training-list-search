using SimpleList.Domain.Entities;
using SimpleList.WebUI.Domain.Repository;
using SimpleList.WebUI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SimpleList.WebUI.Controllers
{
    public class ExampleController : BaseController
    {
        private readonly IRepository<ApplicationUser> _userRepository;

        public ExampleController(IRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: /Example/Users (Initially loads the page)
        public async Task<ActionResult> Users()
        {
            // Retrieve all users asynchronously
            var users = await _userRepository.GetAllAsync();

            var orderedUsers = users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ToList();

            var model = new UserListModel
            {
                Users = orderedUsers,
                Search = new UserSearchModel() // Initialize a new search model
            };

            SetNavOption(NavOption.Examples);
            return View(model);
        }

        // POST: /Example/Users (Handles form submission for filtering)
        [HttpPost]
        public async Task<ActionResult> Users(UserSearchModel search)
        {
            // Retrieve all users asynchronously
            var users = await _userRepository.GetAllAsync();

            // Apply filters based on search criteria
            if (!string.IsNullOrEmpty(search.Name))
            {
                users = users
                    .Where(u => u.FirstName.Contains(search.Name) || u.LastName.Contains(search.Name));
            }

            // Order the results
            var orderedUsers = users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ToList();

            var model = new UserListModel
            {
                Users = orderedUsers,
                Search = search // Pass back the search model to retain user input
            };

            SetNavOption(NavOption.Examples);
            return View(model);
        }
    }
}
