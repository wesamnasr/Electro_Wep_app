using MY_API_PROJECT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MY_API_PROJECT.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
        Task AddReviewAsync(Review review);
        Task<Review?> GetReviewByIdAsync(int reviewId);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int reviewId);
    }
}
