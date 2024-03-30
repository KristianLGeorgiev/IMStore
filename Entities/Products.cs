using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMStore.Entities
{
    public class Products
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име")]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [Range(1, 99999)]
        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Моля, въведете линк към снимка")]
        [Display(Name = "Снимка")]
        public string PictureURL { get; set; }

        [Required(ErrorMessage = "Моля, въведете наличност")]
        [Display(Name = "Наличност")]
        public bool Availability { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }

        [NotMapped]
        public virtual ICollection<SelectListItem>? Categories { get; set; }


        [ForeignKey("ProductId")]
        public virtual ICollection<ProductsOrders>? ProductsOrders { get; set; } 

    }
}
