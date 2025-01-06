using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MY_API_PROJECT.Models
{
    public class OrderItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int OrderID { get; set; }  // Foreign Key to Orders

        [Required]
        public int ProductID { get; set; }  // Foreign Key to Products

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        // Navigation properties
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
