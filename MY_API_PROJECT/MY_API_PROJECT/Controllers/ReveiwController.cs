using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MY_API_PROJECT.DTO.ReviewDTOS;
using MY_API_PROJECT.Models;
using MY_API_PROJECT.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MY_API_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // Get all reviews for a product
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            var reviews = await _reviewRepository.GetReviewsByProductIdAsync(productId);
            if (reviews == null || reviews.Count()==0)
            {
                return NotFound("No reviews found for this product.");
            }

            return Ok(reviews);
        }

        // Get a specific review by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            return Ok(review);
        }

        // Add a new review
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDTO reviewCreateDTO)
        {
            if (ModelState.IsValid)
            {
                var review = new Review
                {
                    ProductID = reviewCreateDTO.productId,
                    UserID = reviewCreateDTO.UserID.ToString(),
                    Rating = reviewCreateDTO.Rating,
                    Comment = reviewCreateDTO.comment,
                    CreatedAt = System.DateTime.Now
                };

                await _reviewRepository.AddReviewAsync(review);
                return CreatedAtAction(nameof(GetReviewById), new { id = review.ID }, review);
            }

            return BadRequest(ModelState);
        }

        // Update a review
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDTO reviewDTO)
        {
            if (ModelState.IsValid)
            {
                var existingReview = await _reviewRepository.GetReviewByIdAsync(id);
                if (existingReview == null)
                {
                    return NotFound($"Review with ID {id} not found.");
                }

                // Update the review details
                existingReview.Rating = reviewDTO.Rating;
                existingReview.Comment = reviewDTO.Comment;

                await _reviewRepository.UpdateReviewAsync(existingReview);
                return NoContent(); // No content on successful update
            }

            return BadRequest(ModelState);
        }

        // Delete a review
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            await _reviewRepository.DeleteReviewAsync(id);
            return NoContent(); // No content on successful delete
        }
    }
}


