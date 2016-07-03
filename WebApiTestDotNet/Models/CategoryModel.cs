using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTestDotNet.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreating { get; set; }        
        public virtual ICollection<BookModel> Book { get; set; }
    }
}