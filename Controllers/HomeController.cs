using IMStore.Data;
using IMStore.Entities;
using IMStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace IMStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext applicationDb;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            applicationDb = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            AddBrandsToList brandsToList = new AddBrandsToList();
            var brands = await GetCurrentBrands();
            brandsToList.Brands = brands;

            return View(brandsToList.Brands);
        }

        private async Task<List<Brands>> GetCurrentBrands()
        {
            var brands = await (
                from brandsList in applicationDb.Brands
                select new Brands
                {
                    Id = brandsList.Id,
                    Name = brandsList.Name,
                    Description = brandsList.Description,
                    BrandLogo = brandsList.BrandLogo
                }).Distinct().ToListAsync();

            return brands;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
