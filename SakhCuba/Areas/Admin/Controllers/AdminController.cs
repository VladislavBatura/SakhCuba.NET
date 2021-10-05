using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakhCuba.Models;

namespace SakhCuba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly SakhCubaContext _context;

        public AdminController(SakhCubaContext context)
        {
            _context = context;
        }

        [Route("[area]")]
        public async Task<IActionResult> Index(string searchString,
            int? decision, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            if (_context.Applications.Count() != 0)
            {
                int pageSize = 10;
                

                IQueryable<Application> app = _context.Applications.Include(u => u.Decision);

                //фильтрация
                if(decision != null && decision != 0)
                {
                    app = app.Where(p => p.DecisionId == decision);
                }

                if(!String.IsNullOrEmpty(searchString))
                {
                    app = app.Where(p => p.Nickname.Contains(searchString));
                }

                //сортировка
                switch (sortOrder)
                {
                    case SortState.NameAsc:
                        app = app.OrderBy(a => a.Name);
                        break;
                    case SortState.NameDesc:
                        app = app.OrderByDescending(a => a.Name);
                        break;
                    case SortState.NicknameAsc:
                        app = app.OrderBy(a => a.Nickname);
                        break;
                    case SortState.NicknameDesc:
                        app = app.OrderByDescending(a => a.Nickname);
                        break;
                    case SortState.DiscordNameAsc:
                        app = app.OrderBy(a => a.DiscordName);
                        break;
                    case SortState.DiscordNameDesc:
                        app = app.OrderByDescending(a => a.DiscordName);
                        break;
                    case SortState.AgeAsc:
                        app = app.OrderBy(a => a.Age);
                        break;
                    case SortState.AgeDesc:
                        app = app.OrderByDescending(a => a.Age);
                        break;
                    case SortState.DecisionAsc:
                        app = app.OrderBy(a => a.Decision);
                        break;
                    case SortState.DecisionDesc:
                        app = app.OrderByDescending(a => a.Decision);
                        break;
                    default:
                        app = app.OrderBy(a => a.Name);
                        break;
                }

                //пагинация
                var count = await app.CountAsync();
                var items = await app.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                IndexViewModel viewModel = new IndexViewModel
                {
                    Applications = items,
                    SortViewModel = new SortViewModel(sortOrder),
                    FilterViewModel = new FilterViewModel(_context.Decisions.ToList(), decision, searchString),
                    PageViewModel = new PageViewModel(count, page, pageSize)
                };

                return View(viewModel);
            }
            return View(await _context.Applications.ToListAsync());

        }

        [Route("[area]/[action]")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                AdminEditViewModel editViewModel = new AdminEditViewModel
                {
                    Application = await _context.Applications.FirstOrDefaultAsync(p => p.Id == id)
                };
                if (editViewModel.Application != null)
                {
                    editViewModel.Decisions = await _context.Decisions.ToListAsync();
                    return View(editViewModel);
                }
                //Application app = await _context.Applications.Include(u => u.Decision).FirstOrDefaultAsync(p => p.Id == id);
                //if (app != null)
                //{
                //    return View(app);
                //}
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[area]/[action]")]
        public async Task<IActionResult> Details(Application application)
        {
            var app = new Application() { Id = application.Id, DecisionId = application.DecisionId };
            _context.Applications.Attach(app);
            _context.Entry(app).Property(x => x.DecisionId).IsModified = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Route("[area]/[action]")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                AdminEditViewModel editViewModel = new AdminEditViewModel
                {
                    Application = await _context.Applications.FirstOrDefaultAsync(p => p.Id == id)
                };
                if (editViewModel.Application != null)
                {
                    editViewModel.Decisions = await _context.Decisions.ToListAsync();
                    return View(editViewModel);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[area]/[action]")]
        public async Task<IActionResult> Edit(Application application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        [Route("[area]/[action]")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Application app = await _context.Applications.FirstOrDefaultAsync(p => p.Id == id);
                if (app != null)
                {
                    return View(app);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[area]/[action]")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Application app = new Application { Id = id.Value };
                _context.Entry(app).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        //public async Task<IActionResult> NewsIndex()
        //{

        //}
    }
}
