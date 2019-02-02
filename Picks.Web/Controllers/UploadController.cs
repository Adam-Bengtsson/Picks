using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Picks.Web.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Upload()
        {
            return View();
        }
    }
}