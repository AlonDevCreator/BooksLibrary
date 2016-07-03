using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebApplicationBookShop.Models
{
    public class Book : ObjectWithId
    {
        
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public virtual ICollection<Category> Category { get; set; }
    }
}