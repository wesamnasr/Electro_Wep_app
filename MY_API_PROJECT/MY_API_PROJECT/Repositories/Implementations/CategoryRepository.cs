using MY_API_PROJECT.Models;
using MY_API_PROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MY_API_PROJECT.DTO;
using MY_API_PROJECT.DTO.CategotyDTOS;
using MY_API_PROJECT.DTO.productDTOS;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDBContext _context;

    public CategoryRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Select(c => new CategoryDTO
            {
                Id = c.ID,
                Name = c.Name,
                Description = c.Description
            })
            .ToListAsync();
    }

    public async Task<CategoryDetailsDTO?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories
            .Where(c => c.ID == id)
            .Select(c => new CategoryDetailsDTO
            {
                Id = c.ID,
                Name = c.Name,
                Description = c.Description,
                Products = c.Products.Select(p => new ProductDTO
                {
                    ID = p.ID,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}

