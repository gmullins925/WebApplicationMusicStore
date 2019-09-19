using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationMusicStore.Data;
using WebApplicationMusicStore.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApplicationMusicStore.Controllers
{
    public class CustomersController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IHostingEnvironment _env;

        public CustomersController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
			_env = env;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

		// GET: Customers/Details/5
		public async Task<IActionResult> Recommendations(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var customer = await _context.Customers
				.FirstOrDefaultAsync(m => m.ID == id);

			var songs = _context.Song.Where(s => s.Genre == customer.FavoriteGenre).ToList();

			ViewBag.CustomerName = customer.Name;
			ViewBag.Title = "Recommendations";
			ViewBag.Genre = customer.FavoriteGenre;

			if (songs.Count == 0)
			{
				ViewBag.Description = "No Recommendations for Customer";
			}

			if (customer == null)
			{
				return NotFound();
			}

			return View(songs);
		}

		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Age,FavoriteGenre,FavoriteArtist,ImagePath")] Customer customer, IFormFile file)
        {
			if (file != null)
			{
				var fileName = Path.GetFileName(file.FileName);
				var path = _env.WebRootPath + "\\uploads\\self\\" + fileName;

				using (var stream = new FileStream(path, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}
				customer.ImagePath = "uploads/self/" + fileName;
			}
			if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Age,FavoriteGenre,FavoriteArtist,ImagePath")] Customer customer)
        {
            if (id != customer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.ID))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }
    }
}
