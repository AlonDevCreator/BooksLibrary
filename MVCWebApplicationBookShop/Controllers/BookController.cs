using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWebApplicationBookShop.Helpers;
using Newtonsoft.Json;
using WebApiTestDotNet.Controllers;
using System.Net.Http;
using System.Net.Http.Headers;
using MVCWebApplicationBookShop.Models;
using System.Web.Script.Serialization;
using System.Net.Http.Formatting;
using System.Diagnostics;

namespace MVCWebApplicationBookShop.Controllers
{
    public class BookController : Controller
    {
        private string urlBooks = "api/Books/";
        private string urlCategories = "api/Categories/";
        // GET: Book
        [HandleError()]
        public ActionResult Index() 
        {
            var listBook = new List<Book>();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];
            if (ApiHelpers.GetData(urlBooks, out data, UserName, Password))
            {
                listBook = JsonConvert.DeserializeObject<List<Book>>(Convert.ToString(data));
            }            
            return View(listBook);
        }
        // GET: Book/Details/5
        public ActionResult Details(int id) 
        {
            Book book = new Book();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];

            if (ApiHelpers.GetData(urlBooks + id, out data, UserName, Password)) 
            {
                book = JsonConvert.DeserializeObject<Book>(Convert.ToString(data));
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create() 
        {
            List<Category> categories = new List<Category>();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];

            if (ApiHelpers.GetData(urlCategories, out data, UserName, Password)) 
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(data));
            }
            ViewBag.listNamesOfAllCategories = categories;
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book book,int[] categoryIds) 
        {
            try
            {
                book.Category = categoryIds.Select(id => new Category() { Id = id }).ToList();
                string Password = (string)Session["Password"];
                string UserName = (string)Session["UserName"];

                HttpResponseMessage response = ApiHelpers.PostPut(urlBooks, book, UserName, Password, Method.PostAsJsonAsync);
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Book/Edit/5
        public ActionResult Edit(int id)  
        {
            Book book = new Book();
            List<Category> categories = new List<Category>();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];
            if (ApiHelpers.GetData(urlBooks + id, out data, UserName, Password))
            {
                book = JsonConvert.DeserializeObject<Book>(Convert.ToString(data));
            }
            if (ApiHelpers.GetData(urlCategories, out data, UserName, Password)) 
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(data));
            }                    
            ViewBag.listNamesOfAllCategories = categories;
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(Book book, int[] categoryIds) 
        {
            try
            {
                book.Category = categoryIds.Select(id => new Category() { Id = id }).ToList();                
                string Password = (string)Session["Password"];
                string UserName = (string)Session["UserName"];

                HttpResponseMessage response = ApiHelpers.PostPut(urlBooks, book, UserName, Password, Method.PutAsJsonAsync);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            Book book = new Book();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];

            if (ApiHelpers.GetData(urlBooks + id, out data, UserName, Password)) 
            {
                book = JsonConvert.DeserializeObject<Book>(Convert.ToString(data));
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Book book)
        {
            try
            {
                string Password = (string)Session["Password"];
                string UserName = (string)Session["UserName"];
                HttpResponseMessage response = ApiHelpers.DeleteData(urlBooks, book.Id, UserName, Password);                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
