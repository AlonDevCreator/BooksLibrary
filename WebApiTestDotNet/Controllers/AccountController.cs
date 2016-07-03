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
    public class AccountController : ApiController
    {
        //[Authorize(Roles = "Admin")]
        //public IHttpActionResult PostRegister(User user)
        //{
        //    //user to DB
        //    return Ok();
        //}


        //ПЕРЕДЕЛАТЬ
        public IHttpActionResult GetLogin()
        {
            return Ok();
        }
    }
}