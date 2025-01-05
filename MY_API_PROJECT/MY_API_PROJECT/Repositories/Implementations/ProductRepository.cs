using MY_API_PROJECT.Data;
using MY_API_PROJECT.DTO;
using MY_API_PROJECT.Models;
using MY_API_PROJECT.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MY_API_PROJECT.DTO.productDTOS;

namespace MY_API_PROJECT.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _context;

        public ProductRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return products.Select(p => new ProductDTO
                {
                    ID = p.ID,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryID = p.CategoryID
                }).ToList();
            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<ProductDetailsDTO?> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.ID == id)
                    .Select(p => new ProductDetailsDTO
                    {
                        Id= p.ID,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        StockQuantity = p.StockQuantity,
                        CategoryID = p.CategoryID,
                        CategoryName = p.Category.Name,
                        CreatedAt = p.CreatedAt,
                      
                    })
                    .FirstOrDefaultAsync();

                return product;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"An error occurred while retrieving product with ID {id}.", ex);
            }
        }

        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new ArgumentNullException(nameof(product), "Product cannot be null");
                }

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
             
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
              
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }
    }
}
