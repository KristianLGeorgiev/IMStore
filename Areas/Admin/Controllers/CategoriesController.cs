using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMStore.Data;
using IMStore.Entities;
using IMStore.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace IMStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index(int categoryId)
        {
            //get all the categories of products that each brand has
            List<Categories> listCategories = await (from category in _context.Categories
                                                     where category.BrandId == categoryId
                                                     select new Categories
                                                     {
                                                         Id = category.Id,
                                                         Name = category.Name,
                                                         BrandId = categoryId,
                                                     }).ToListAsync();
            ViewBag.BrandId = categoryId;

            return View(listCategories);
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Admin/Categories/Create
        public async Task<IActionResult> Create(int categoryId)
        {
            List<Brands> brands = await _context.Brands.ToListAsync();

            Categories categories = new Categories
            {
                Id = categoryId,
                Brands = brands.ConvertToSelectList(0)
            };

            return View(categories);
        }
        

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BrandId")] Categories categories)
        {
            var dups = _context.Categories.Where(a => a.Name == categories.Name && a.BrandId == categories.BrandId).Any();

            if (ModelState.IsValid && !dups)
            {
                _context.Add(categories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { categoryId = categories.BrandId });
            }
            else
            {
                return RedirectToAction(nameof(Index), new { categoryId = categories.BrandId });
            }

        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Brands> brands = await _context.Brands.ToListAsync();

            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            categories.Brands = brands.ConvertToSelectList(categories.BrandId);

            return View(categories);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BrandId")] Categories categories)
        {
            if (id != categories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {categoryId = categories.BrandId});
            }
            return View(categories);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories != null)
            {
                _context.Categories.Remove(categories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {categoryId = categories.BrandId});
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
