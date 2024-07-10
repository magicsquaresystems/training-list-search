using SimpleList.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleList.WebUI.Controllers
{
    public class ExampleController : BaseController
    {
        // GET: /users
        public ActionResult Users(UserSearchModel search)
        {
            var users = db.Users
                .OrderBy(o => o.LastName)
                .ThenBy(o => o.FirstName)
                .ToList();
            var model = new UserListModel {
                Users = users,
                Search = search
            };

            SetNavOption(NavOption.Examples);
            return View(model);
        }
    }
}