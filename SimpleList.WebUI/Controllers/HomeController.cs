using SimpleList.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleList.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /
        public ActionResult Index()
        {
            return View();
        }
    }
}