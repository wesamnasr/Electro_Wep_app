using MY_API_PROJECT.DTO.productDTOS;

namespace MY_API_PROJECT.DTO.CategotyDTOS
{
    public class CategoryDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }

}
