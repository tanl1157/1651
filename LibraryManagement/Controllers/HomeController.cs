using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class HomeController : BasicAuthorizationController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Order");
        }

        public ActionResult _LeftMenu()
        {
            return View();
        }

        public ActionResult _TopMenu()
        {
            return View();
        }

        public ActionResult _Footer()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}