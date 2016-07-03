using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebApplicationBookShop.Models;
using MVCWebApplicationBookShop.Helpers;

namespace MVCWebApplicationBookShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Accaunt
        [HttpGet]
        public ActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            Session["Password"] = user.Password;
            Session["UserName"] = user.UserName;
            object data;
            if(ApiHelpers.GetData("api/Account/", out data, user.UserName, user.Password))
            {

                ViewBag.Result = "You are logged in";
            }
            else
            {
                ViewBag.Result = "The user name or password provided is incorrect.";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return View();
        }
    }
}