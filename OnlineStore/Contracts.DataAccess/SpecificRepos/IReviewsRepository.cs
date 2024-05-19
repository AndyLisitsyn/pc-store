using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Contracts.DataAccess.SpecificRepos
{
    public interface IReviewsRepository : IRepository<Review>
    {
        Task<List<Review>> GetReviewsByProductId(int productId);
        Task<int> GetReviewsCountByProductId(int productId);
        Task<bool> IsReviewPostedForProductByUser(int productId, string userId);
        Task<List<Review>> GetReviewsPageByProductId(int productId, int pageNumber, int pageSize);
        Task<int> GetAverageRatingByProductId(int productId);
    }
}
