using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakhCuba.Models;

namespace SakhCuba.Controllers
{
    public class HomeController : Controller
    {
        private readonly SakhCubaContext _context;

        public HomeController(SakhCubaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> News()
        {
            return View();
        }

        public async Task<IActionResult> Rules()
        {
            return View();
        }

        public IActionResult Application()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Application(Application application)
        {
            if(ModelState.IsValid)
            {
                var decisionHandler = await _context.Decisions.FirstOrDefaultAsync(d => d.DecisionId == 4);
                application.Decision = new Decision { DecisionId = decisionHandler.DecisionId, DecisionName = decisionHandler.DecisionName};
                application.DecisionId = decisionHandler.DecisionId;
                //на будущее - поможет высматривать самых чудаковатых игроков
                var ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4();
                var time = DateTime.Today;
                application.Date = time;
                application.IP = ip.ToString();
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();
                return View();
            }
            else
            {
                return View(application);
            }
        }
    }
}
