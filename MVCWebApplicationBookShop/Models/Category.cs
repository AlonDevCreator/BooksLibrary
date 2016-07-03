using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebApplicationBookShop.Models
{
    public class Category : ObjectWithId
    {
        public string Name { get; set; }
        public DateTime DateOfCreating { get; set; }
        public virtual ICollection<Book> Book { get; set; }
    }
}