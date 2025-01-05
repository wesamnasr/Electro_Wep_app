using MY_API_PROJECT.DTO.productDTOS;

namespace MY_API_PROJECT.Repositories.Interfaces
{
    public interface IProductRepository
    {
     

        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDetailsDTO?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }

}
