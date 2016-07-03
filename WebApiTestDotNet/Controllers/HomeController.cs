using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiTestDotNet.Models;
using System.Data.Entity;
namespace WebApiTestDotNet.Controllers
{
    public class HomeController : Controller
    {
        BookShopDBContainer db = new BookShopDBContainer();
        
        public ActionResult Index()
        {
            List<CategoryModel> categories = db.Categories
               .Select(c => new CategoryModel
               {
                   Id = c.Id,
                   Name = c.Name,
                   DateOfCreating = c.DateOfCreating,
                   Book = c.Book
                   .Select(b => new BookModel
                   {
                       Id = b.Id,
                       Name = b.Name,
                       Author = b.Author,
                       ISBN = b.ISBN
                   }).ToList()
               }).ToList();
            ViewBag.BookCount = db.Books.Where(b => !b.Category.Any()).Count();
            return View(categories);

        }
    }
}
