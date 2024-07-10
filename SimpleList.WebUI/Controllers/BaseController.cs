using SimpleList.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleList.WebUI.Controllers
{
    public class BaseController : Controller
    {
        // All shared objects go here
        protected ApplicationDbContext db;

        public BaseController()
        {
            db = new ApplicationDbContext();
        }

        protected void SetNavOption(NavOption navId)
        {
            ViewBag.NavId = navId;
        }
    }
}