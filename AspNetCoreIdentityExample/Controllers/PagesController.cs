using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityExample.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        [Authorize(Roles = "Tor")]
        public IActionResult Page1()
        {
            return View();
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Page2()
        {
            return View();
        }

        [Authorize(Roles = "Moderator", Policy = "TimeControl")]
        public IActionResult Page3()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Page4()
        {
            return View();
        }
    }
}