using IMStore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMStore.Entities
{
    public class Categories : IPrimaryProperties
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име")]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Моля, изберете фирма")]
        [Display(Name = "Фирма")]
        public int BrandId { get; set; }

        [NotMapped]
        public virtual ICollection<SelectListItem>? Brands { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ICollection<Products>? Products { get; set; }


    }
}
