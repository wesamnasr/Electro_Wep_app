using MY_API_PROJECT.DTO.ReviewDTOS;

namespace MY_API_PROJECT.DTO.productDTOS
{
    public class ProductDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } // Name of the category
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Reviews
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

        // Wishlists
        public List<WishlistDTO> Wishlists { get; set; } = new List<WishlistDTO>();
    }

}
