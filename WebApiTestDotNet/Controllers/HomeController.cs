using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiTestDotNet.Models;

namespace WebApiTestDotNet.Controllers
{
    public class HomeController : Controller
    {
        BookShopDBContainer db = new BookShopDBContainer();

        public ActionResult Index()
        {
            var categories = db.Categories // в класс, а не анонимный
               .Select(c => new {
                   Id = c.Id,
                   Name = c.Name,
                   DateOfCreating = c.DateOfCreating,
                   Book = c.Book
                   .Select(b => new {
                       Id = b.Id,
                       Name = b.Name,
                       Author = b.Author,
                       ISBN = b.ISBN,
                   }).ToList()
               });
            

            ViewBag.BookCount = db.Books.Where(b => !b.Category.Any()).Count();
            return View(categories);

        }
    }
}
