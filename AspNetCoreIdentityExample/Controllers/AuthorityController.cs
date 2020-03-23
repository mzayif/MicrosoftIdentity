using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityExample.Controllers
{
    public class AuthorityController : Controller
    {
        public IActionResult Page()
        {
            return View();
        }
    }
}