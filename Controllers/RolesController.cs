using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMStore.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _manager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
                _manager = roleManager;
        }
        //list all roles
        public IActionResult Index()
        {
            var roles = _manager.Roles;
            return View(roles);
        }

        //load create page
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            //Check if the role exists before adding it - prevent dups
            if (!_manager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
                _manager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
            }
            else
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
