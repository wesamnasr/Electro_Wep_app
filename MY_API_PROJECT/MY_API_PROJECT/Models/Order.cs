using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MY_API_PROJECT.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string UserID { get; set; }  // Foreign Key to Users

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
