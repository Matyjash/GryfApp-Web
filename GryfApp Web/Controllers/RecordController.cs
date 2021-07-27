using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GryfApp_Web.Controllers
{
    public class RecordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
