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

namespace SakhCuba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly SakhCubaContext _context;
        private readonly IHostingEnvironment _env;

        public NewsController(SakhCubaContext context, IHostingEnvironment env)
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

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[area]/[controller]/[action]")]
        public async Task<IActionResult> Create(NewsViewModel newsViewModel)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _env.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(newsViewModel.Image.ImageFile.FileName);
                string extension = Path.GetExtension(newsViewModel.Image.ImageFile.FileName);
                newsViewModel.Image.ImageName = fileName = fileName + DateTime.Now.ToString("yymmss") + extension;
                string path = Path.Combine(wwwRootPath + "/images/news", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await newsViewModel.Image.ImageFile.CopyToAsync(filestream);
                }
                _context.News.Add(newsViewModel.News);
                newsViewModel.Image.NewsId = newsViewModel.News.Id;
                _context.Images.Add(newsViewModel.Image);
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

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[area]/[controller]/[action]")]
        public async Task<IActionResult> Edit(int id, [Bind("NewsId,Header,FirstColumn,SecondColumn,ThirdColumn")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            //NewsViewModel newsViewModel = new NewsViewModel();
            //newsViewModel.News = await _context.News.FirstOrDefaultAsync(i => i.Id == id);
            //newsViewModel.Image = await _context.Images.FirstOrDefaultAsync(i => i.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("[area]/[controller]/[action]")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //почему-то если передать картинку в гет, то в посте id всегда будет нулевым
            var news = await _context.News.FindAsync(id);
            var image = await _context.Images.FirstAsync(i => i.NewsId == id);

            var imagePath = Path.Combine(_env.WebRootPath, "images/news", image.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _context.Images.Remove(image);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
