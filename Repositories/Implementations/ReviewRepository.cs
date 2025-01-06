using MY_API_PROJECT.Data;
using MY_API_PROJECT.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MY_API_PROJECT.Repositories.Interfaces;

namespace MY_API_PROJECT.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDBContext _context;

        public ReviewRepository(AppDBContext context)
        {
            _context = context;
        }

    
        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId)
        {
            return await _context.Reviews
                                 .Where(r => r.ProductID == productId)
                                 .Include(r => r.User)
                                 .ToListAsync();
        }

     
        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

     
        public async Task<Review?> GetReviewByIdAsync(int reviewId)
        {
            return await _context.Reviews
                                 .Include(r => r.User)
                                 .FirstOrDefaultAsync(r => r.ID == reviewId);
        }

      
        public async Task UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

      
        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
