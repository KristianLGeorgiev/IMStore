using IMStore.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMStore.Entities
{
    public class Brands : IPrimaryProperties
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име на фирмата.")]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Моля, въведете линк към лого")]
        [Display(Name = "Линк към лого")]
        public string BrandLogo {  get; set; }

        [ForeignKey("BrandId")]
        public virtual ICollection<Categories>? Categories { get; set; }

    }
}
