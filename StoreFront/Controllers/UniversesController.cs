using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.Controllers
{
    public class UniversesController : Controller
    {
        private readonly StorefrontContext _context;

        public UniversesController(StorefrontContext context)
        {
            _context = context;
        }

        // GET: Universes
        public async Task<IActionResult> Index()
        {
              return _context.Universes != null ? 
                          View(await _context.Universes.ToListAsync()) :
                          Problem("Entity set 'StorefrontContext.Universes'  is null.");
        }

        // GET: Universes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Universes == null)
            {
                return NotFound();
            }

            var universe = await _context.Universes
                .FirstOrDefaultAsync(m => m.UniverseId == id);
            if (universe == null)
            {
                return NotFound();
            }

            return View(universe);
        }

        // GET: Universes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Universes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UniverseId,UniverseName")] Universe universe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(universe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(universe);
        }

        // GET: Universes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Universes == null)
            {
                return NotFound();
            }

            var universe = await _context.Universes.FindAsync(id);
            if (universe == null)
            {
                return NotFound();
            }
            return View(universe);
        }

        // POST: Universes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UniverseId,UniverseName")] Universe universe)
        {
            if (id != universe.UniverseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(universe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniverseExists(universe.UniverseId))
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
            return View(universe);
        }

        // GET: Universes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Universes == null)
            {
                return NotFound();
            }

            var universe = await _context.Universes
                .FirstOrDefaultAsync(m => m.UniverseId == id);
            if (universe == null)
            {
                return NotFound();
            }

            return View(universe);
        }

        // POST: Universes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Universes == null)
            {
                return Problem("Entity set 'StorefrontContext.Universes'  is null.");
            }
            var universe = await _context.Universes.FindAsync(id);
            if (universe != null)
            {
                _context.Universes.Remove(universe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniverseExists(int id)
        {
          return (_context.Universes?.Any(e => e.UniverseId == id)).GetValueOrDefault();
        }
    }
}
