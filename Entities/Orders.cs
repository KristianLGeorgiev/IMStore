using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMStore.Entities
{
    public class Orders
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }

        [ForeignKey("OrderId")]
        public virtual ICollection<ProductsOrders> ProductsOrders { get; set; }
    }
}
