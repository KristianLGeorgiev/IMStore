using IMStore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMStore.Data
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(250)]
        public string SecondName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<Orders> Orders { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brands> Brands { get; set; }
        public DbSet<Categories> Categories { get; set; }   
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet <ProductsOrders> ProductsOrders { get; set; }

    }
}
