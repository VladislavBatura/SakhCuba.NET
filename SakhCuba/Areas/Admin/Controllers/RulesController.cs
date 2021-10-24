using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SakhCuba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RulesController : Controller
    {
        [Route("[area]/[controller]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
