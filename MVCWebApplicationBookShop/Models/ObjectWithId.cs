using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebApplicationBookShop.Models
{
    public abstract class ObjectWithId
    {
        public int Id { get; set; }
    }
}