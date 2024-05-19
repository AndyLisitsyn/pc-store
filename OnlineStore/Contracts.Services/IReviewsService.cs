using System.Collections.Generic;
using System.Threading.Tasks;
using NonDomain;
using Services.Dto;

namespace Contracts.Services
{
    public interface IReviewsService
    {
        Task<int> GetReviewsCountByProductId(int productId);
        Task<int> GetAverageRatingByProductId(int productId);
        Task<OperationResult> AddReview(ReviewDto review);
        Task<bool> IsReviewPostedForProductByUser(int productId, string userId);
        Task<List<ReviewDto>> GetReviewsPageByProductId(int productId, int pageNumber, int pageSize);
    }
}
