using System.Collections.Generic;
using Model;
using Services.Dto;

namespace WEB.ViewModels.ProductViewModules
{
    public class DetailsViewModel
    {
        public ProductDto Product { get; set; }
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
        public int AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public ReviewDto Review { get; set; }
        public bool IsReviewPostedByUser { get; set; }
    }
}
