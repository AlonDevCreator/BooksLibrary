using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiTestDotNet.Models;

namespace WebApiTestDotNet.Controllers
{
    public class CategoriesController : ApiController
    {
        private BookShopDBContainer db = new BookShopDBContainer();

        // GET: api/Categories
        public IQueryable<object> GetCategories()
        {
            var categories = db.Categories
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

            return categories.AsQueryable<object>();
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            var category = db.Categories
                .Where(b => b.Id == id)
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
            
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}