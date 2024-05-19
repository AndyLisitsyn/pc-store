using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Model;
using Contracts.DataAccess.SpecificRepos;

namespace DataAccess
{
    public class ReviewsRepository : BaseRepository<Review>, IReviewsRepository
    {
        public ReviewsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public Task<List<Review>> GetReviewsByProductId(int productId)
        {
            return _dbSet.AsNoTracking().Where(r => r.ProductId == productId).ToListAsync();
        }

        public Task<int> GetReviewsCountByProductId(int productId)
        {
            return _dbSet.Where(r => r.ProductId == productId).CountAsync();
        }

        public async Task<int> GetAverageRatingByProductId(int productId)
        {
            var result = 0;
            var reviewsCount = await GetReviewsCountByProductId(productId);
            if (reviewsCount != 0)
            {
                var averageRating = await _dbSet.AsNoTracking().Where(r => r.ProductId == productId).SumAsync(r => r.Rating) / (decimal)reviewsCount;
                result = (int)Math.Round(averageRating);
            }
            return result;
        }

        public async Task<List<Review>> GetReviewsPageByProductId(int productId, int pageNumber, int pageSize)
        {
            var reviews = await GetReviewsByProductId(productId);
            reviews.Reverse();
            int totalReviewsCount = await GetReviewsCountByProductId(productId);
            int reviewsCountToSkip = (pageNumber - 1) * pageSize;
            int reviewsCountToTake = pageSize;
            if (pageNumber * pageSize > totalReviewsCount)
            {
                reviewsCountToTake = totalReviewsCount - reviewsCountToSkip;
            }
            return reviews.Skip(reviewsCountToSkip).Take(reviewsCountToTake).ToList();
        }

        public Task<bool> IsReviewPostedForProductByUser(int productId, string userId)
        {
            return _dbSet.AsNoTracking().AnyAsync(r => r.ProductId == productId && r.UserId == userId);
        }
    }
}
