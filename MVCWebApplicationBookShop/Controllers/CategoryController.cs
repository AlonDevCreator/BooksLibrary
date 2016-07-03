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
    public class CategoryController : Controller
    {
        private string urlCategories = "api/Categories/";

        // GET: Category
        [HandleError()]
        public ActionResult Index()
        {
            List<Category> listCategory = new List<Category>();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];
            if (ApiHelpers.GetData(urlCategories, out data, UserName, Password))
            {
                listCategory = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(data));
            }
            return View(listCategory);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            Category category = new Category();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];

            if (ApiHelpers.GetData(urlCategories + id, out data, UserName, Password))
            {
                category = JsonConvert.DeserializeObject<Category>(Convert.ToString(data));
            }
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];
            ApiHelpers.InitializeClient(UserName, Password);
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                string Password = (string)Session["Password"];
                string UserName = (string)Session["UserName"];
                HttpResponseMessage response = ApiHelpers.PostPut(urlCategories, category, UserName, Password, Method.PostAsJsonAsync);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = new Category();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];
            if (ApiHelpers.GetData(urlCategories + id, out data, UserName, Password))
            {
                category = JsonConvert.DeserializeObject<Category>(Convert.ToString(data));
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                string Password = (string)Session["Password"];
                string UserName = (string)Session["UserName"];
                HttpResponseMessage response = ApiHelpers.PostPut(urlCategories, category, UserName, Password, Method.PutAsJsonAsync);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = new Category();
            object data;
            string Password = (string)Session["Password"];
            string UserName = (string)Session["UserName"];

            if (ApiHelpers.GetData(urlCategories + id, out data, UserName, Password))
            {
                category = JsonConvert.DeserializeObject<Category>(Convert.ToString(data));
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                string Password = (string)Session["Password"];
                string UserName = (string)Session["UserName"];
                HttpResponseMessage response = ApiHelpers.DeleteData(urlCategories, category.Id, UserName, Password);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

