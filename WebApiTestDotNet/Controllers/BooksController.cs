using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiTestDotNet.Models;
using System.Web.Security;

namespace WebApiTestDotNet.Controllers
{
    [Authorize]
    public class BooksController : ApiController
    {
        private BookShopDBContainer db = new BookShopDBContainer();

        // GET: api/Books
        [Authorize(Roles = "User,Admin")]
        public IQueryable<object> GetBooks()
        {
            var books = db.Books
                .Select(b => new {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Category = b.Category
                    .Select(c => new {
                        Id = c.Id,
                        Name = c.Name,
                        DateOfCreating = c.DateOfCreating
                    }).AsQueryable<object>()//.ToList()
                });

            return books.AsQueryable<object>();
        }

        // GET: api/Books/5
        [Authorize(Roles = "User,Admin")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            var book = db.Books
                .SingleOrDefault(b => b.Id == id);
                
            if (book == null)
            {
                return NotFound();
            }

            var jsonBook = new
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                ISBN = book.ISBN,
                Category = book.Category
                    .Select(c => new
                    {
                        Id = c.Id,
                        Name = c.Name,
                        DateOfCreating = c.DateOfCreating
                    })
            };
            return Ok(jsonBook);
        }

        // PUT: api/Books/5
        [Authorize(Roles = "User,Admin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(Book book) //--------------------------------
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryIds = book.Category.Select(c => c.Id).ToList();
            var newCategories = db.Categories.Where(c => categoryIds.Any(bc => bc == c.Id)).ToList();

            var oldCategories = db.Categories.Where(c => c.Book.Any(b => b.Id == book.Id)).ToList();

            book.Category = oldCategories;

            //var entry = 
            db.Entry(book).State = EntityState.Modified;

            book.Category = newCategories;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
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

        // POST: api/Books
        [Authorize(Roles = "User,Admin")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book) //--------------------------------
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryIds = book.Category.Select(c => c.Id).ToList();
            var categories = db.Categories.Where(c => categoryIds.Any(bc => bc == c.Id)).ToList();
            book.Category = categories;
            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
       // [ResponseType(typeof(Book))]
        [Authorize(Roles = "User,Admin")]
        public IHttpActionResult DeleteBook(int id) //--------------------------------
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            
            db.Books.Remove(book);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing) //--------------------------------
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id) //--------------------------------
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}