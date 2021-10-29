using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SakhCuba.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace SakhCuba.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly SakhCubaContext _context;
        private readonly IWebHostEnvironment _env;

        public NewsController(SakhCubaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/News
        [Route("[area]/[controller]")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.News.ToListAsync());
        }

        // GET: Admin/News/Details/5
        [Route("[area]/[controller]/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        [Route("[area]/[controller]/Create")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[area]/[controller]/[action]")]
        public async Task<IActionResult> Create(NewsViewModel newsViewModel)
        {
            if (ModelState.IsValid)
            {
                News news = new News
                {
                    Header = newsViewModel.Header,
                    FirstColumn = newsViewModel.FirstColumn,
                    SecondColumn = newsViewModel.SecondColumn,
                    ThirdColumn = newsViewModel.ThirdColumn,
                    Picture = ImageConvertion(newsViewModel.Picture)
                };
                _context.News.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsViewModel);
        }

        // GET: Admin/News/Edit/5
        [Route("[area]/[controller]/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            News news = await _context.News.FindAsync(id);
            NewsEditViewModel n = new NewsEditViewModel
            {
                Id = news.Id.ToString(),
                Header = news.Header,
                FirstColumn = news.FirstColumn,
                SecondColumn = news.SecondColumn,
                ThirdColumn = news.ThirdColumn,
                Picture = news.Picture
            };

            return View(n);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[area]/[controller]/[action]")]
        public async Task<IActionResult> Edit(NewsEditViewModel news)
        {
            if (ModelState.IsValid)
            {
                News n = new News
                {
                    Id = int.Parse(news.Id),
                    Header = news.Header,
                    FirstColumn = news.FirstColumn,
                    SecondColumn = news.SecondColumn,
                    ThirdColumn = news.ThirdColumn
                };

                try
                {
                    if (news.NewPicture != null)
                    {
                        n.Picture = ImageConvertion(news.NewPicture);
                        _context.News.Update(n);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.News.Attach(n);
                        _context.Entry(n).Property(x => x.Picture).IsModified = false;
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(n.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        [HttpGet]
        [Route("[area]/[controller]/[action]")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FirstOrDefaultAsync(m => m.Id == id);

            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost]
        [Route("[area]/[controller]/[action]")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if(id != null)
            {
                News news = new News { Id = id.Value };
                _context.Entry(news).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        private byte[] ImageConvertion(IFormFile image)
        {
            if (image != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)image.Length);
                }
                return imageData;
            }
            return null;
        }
    }
}
