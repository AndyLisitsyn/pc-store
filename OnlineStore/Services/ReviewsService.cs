using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DataAccess.SpecificRepos;
using AutoMapper;
using NonDomain;
using Services.Dto;
using Model;
using Contracts.Services;

namespace Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IMapper _mapper;

        public ReviewsService(
            IReviewsRepository reviewsRepository,
            IMapper mapper)
        {
            _reviewsRepository = reviewsRepository;
            _mapper = mapper;
        }

        public Task<int> GetReviewsCountByProductId(int productId)
        {
            return _reviewsRepository.GetReviewsCountByProductId(productId);
        }

        public Task<int> GetAverageRatingByProductId(int productId)
        {
            return _reviewsRepository.GetAverageRatingByProductId(productId);
        }

        public Task<OperationResult> AddReview(ReviewDto review)
        {
            return _reviewsRepository.InsertAsync(_mapper.Map<Review>(review), true);
        }

        public Task<bool> IsReviewPostedForProductByUser(int productId, string userId)
        {
            return _reviewsRepository.IsReviewPostedForProductByUser(productId, userId);
        }

        public async Task<List<ReviewDto>> GetReviewsPageByProductId(int productId, int pageNumber, int pageSize)
        {
            var reviews = await _reviewsRepository.GetReviewsPageByProductId(productId, pageNumber, pageSize);
            return _mapper.Map<List<ReviewDto>>(reviews);
        }
    }
}
