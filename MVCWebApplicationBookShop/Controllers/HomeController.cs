using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebApplicationBookShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Проект MVC Web Application и REST API.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Автор проекта Барабаш Алёна Петровна.";

            return View();
        }
    }
}