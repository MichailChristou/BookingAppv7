using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingAppv7.Models;

namespace BookingAppv7.Controllers
{
    public class Showings2Controller : Controller
    {
        private readonly TestDbContext _context;

        public Showings2Controller(TestDbContext context)
        {
            _context = context;
        }

        // GET: Showings2
        public async Task<IActionResult> Index()
        {
            var testDbContext = _context.Showings.Include(s => s.Movie);
            return View(await testDbContext.ToListAsync());
        }

        // GET: Showings2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showings
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showing == null)
            {
                return NotFound();
            }

            return View(showing);
        }

        // GET: Showings2/Create
        public IActionResult Create()
        {
            ViewData["Movieid"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: Showings2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Movieid,Datet,Seats")] Showing showing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(showing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Movieid"] = new SelectList(_context.Movies, "Id", "Id", showing.Movieid);
            return View(showing);
        }

        // GET: Showings2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showings.FindAsync(id);
            if (showing == null)
            {
                return NotFound();
            }
            ViewData["Movieid"] = new SelectList(_context.Movies, "Id", "Id", showing.Movieid);
            return View(showing);
        }

        // POST: Showings2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Movieid,Datet,Seats")] Showing showing)
        {
            if (id != showing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(showing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowingExists(showing.Id))
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
            ViewData["Movieid"] = new SelectList(_context.Movies, "Id", "Id", showing.Movieid);
            return View(showing);
        }

        // GET: Showings2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showing = await _context.Showings
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showing == null)
            {
                return NotFound();
            }

            return View(showing);
        }

        // POST: Showings2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showing = await _context.Showings.FindAsync(id);
            if (showing != null)
            {
                _context.Showings.Remove(showing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowingExists(int id)
        {
            return _context.Showings.Any(e => e.Id == id);
        }
    }
}
