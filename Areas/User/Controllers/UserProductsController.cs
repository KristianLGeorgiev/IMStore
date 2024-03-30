using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMStore.Data;
using IMStore.Entities;
using static System.Net.Mime.MediaTypeNames;
using IMStore.Models;

namespace IMStore.Areas.User.Controllers
{
    [Area("User")]
    public class UserProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User/UserProducts
        public async Task<IActionResult> Index(string searchText)
        {
            AddProductsToList addProductsToList = new AddProductsToList();
            var products = await GetCurrentProducts(searchText);
            addProductsToList.Products = products;
            return View(addProductsToList.Products);
        }

        private async Task<List<Products>> GetCurrentProducts(string searchText)
        {
         
            var products = await (
                from productsList in _context.Products
                join cat in _context.Categories on productsList.CategoryId equals cat.Id
                select new Products
                {
                    Id = productsList.Id,
                    Name = productsList.Name,
                    Price = productsList.Price,
                    Description = productsList.Description,
                    PictureURL = productsList.PictureURL,
                    Availability = productsList.Availability,
                    CategoryName = cat.Name
                }).Distinct().ToListAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                products = (List<Products>)products.Where(p => p.CategoryName == searchText).ToList();
            }

            return products;
        }

    }
}
