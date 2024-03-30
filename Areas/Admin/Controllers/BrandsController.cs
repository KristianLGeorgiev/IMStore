using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMStore.Data;
using IMStore.Entities;
using Microsoft.AspNetCore.Authorization;

namespace IMStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // GET: Admin/Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,BrandLogo")] Brands brands)
        {
            var dups = _context.Brands.Where(a => a.Name == brands.Name).Any();

            if (ModelState.IsValid && !dups)
            {
                _context.Add(brands);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(brands);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = await _context.Brands.FindAsync(id);
            if (brands == null)
            {
                return NotFound();
            }
            return View(brands);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,BrandLogo")] Brands brands)
        {
            if (id != brands.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brands);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandsExists(brands.Id))
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
            return View(brands);
        }

        // GET: Admin/Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brands = await _context.Brands.FindAsync(id);
            if (brands != null)
            {
                _context.Brands.Remove(brands);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandsExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
