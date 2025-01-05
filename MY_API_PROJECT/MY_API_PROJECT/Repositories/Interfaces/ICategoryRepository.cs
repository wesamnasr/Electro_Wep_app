using MY_API_PROJECT.DTO.CategotyDTOS;

namespace MY_API_PROJECT.Repositories.Interfaces
{

    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDetailsDTO?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }

}
