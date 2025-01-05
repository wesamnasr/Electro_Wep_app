using System.ComponentModel.DataAnnotations;


    public class OrderCreateDTO
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one product must be included.")]
        public List<int> ProductIDs { get; set; } = new List<int>();

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0.")]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Order status must not exceed 20 characters.")]
        public string OrderStatus { get; set; } = "Pending";

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }


