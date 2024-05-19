using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Contracts.Services;
using WEB.ViewModels.ProductViewModules;
using System.Linq;
using Model;
using Utils;
using Services.Dto;


namespace WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IReviewsService _reviewsService;
        private readonly IImagesService _imagesService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProductController(
            ILogger<HomeController> logger,
            IProductsService productsService,
            ICategoriesService categoriesService,
            IReviewsService reviewsService,
            IImagesService imagesService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _productsService = productsService;
            _categoriesService = categoriesService;
            _reviewsService = reviewsService;
            _imagesService = imagesService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("Index/{categoryId}/{page}")]
        public async Task<JsonResult> Index(int categoryId, int page = 1)
        {
            var json = new
            {
                Category = await _categoriesService.GetCategoryById(categoryId),
                Products = await _productsService.GetProductsPageByCategoryId(categoryId, page, ProductConstants.ProductsPageSize),
                PagesCount = await _productsService.GetProductsPagesCountByCategoryId(categoryId, ProductConstants.ProductsPageSize)
            };
            return Json(json);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ProductDto> GetProductById(int id)
        {
            return await _productsService.GetProductById(id);
        }

        [HttpGet("GetDeletedProducts")]
        public async Task<IEnumerable<ProductDto>> GetDeletedProducts()
        {
            return await _productsService.GetDeletedProducts();
        }

        [HttpGet("Details/{id}")]
        public async Task<JsonResult> Details(int id)
        {
            int totalReviewsCount = await _reviewsService.GetReviewsCountByProductId(id);
            var user = _signInManager.IsSignedIn(User) ? await _userManager.FindByEmailAsync(User.Identity.Name) : null;
            var detailsViewModel = new DetailsViewModel()
            {
                Product = await _productsService.GetProductById(id),
                ReviewsCount = totalReviewsCount,
                IsReviewPostedByUser = user != null && await _reviewsService.IsReviewPostedForProductByUser(id, user.Id),
                Review = new ReviewDto() { ProductId = id }
            };
            if (totalReviewsCount != 0)
            {
                detailsViewModel.Reviews = await _reviewsService.GetReviewsPageByProductId(id, 1, ReviewConstants.ReviewsLoadingPageSize);
                detailsViewModel.AverageRating = await _reviewsService.GetAverageRatingByProductId(id);
            }
            return Json(detailsViewModel);
        }

        [HttpGet("LoadMoreReviews/{id}/{page}")]
        public async Task<ActionResult> LoadMoreReviews(int id, int page)
        {
            var reviews = await _reviewsService.GetReviewsPageByProductId(id, page, ReviewConstants.ReviewsLoadingPageSize);
            return PartialView("LoadedReviewsPartial", reviews);
        }

        [HttpPost("PostReview")]
        public async Task<ActionResult> PostReview(ReviewDto review)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                review.UserId = user.Id;
                review.Date = DateTime.Now;
                var operationResult = await _reviewsService.AddReview(review);
                if (!operationResult.IsSuccess)
                {
                    _logger.LogInformation($"An error occurred while adding a review:\n{operationResult.Message}");
                    return BadRequest("Posting review error");
                }
                return Ok();
            }
            return BadRequest("Posting review error");
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                product.Images = product.Images.Where(i => i.Path != null).ToList();
                var operationResult = await _productsService.AddProduct(product);
                if (!operationResult.IsSuccess)
                {
                    _logger.LogInformation($"An error occurred while adding a product:\n{operationResult.Message}");
                    return BadRequest("Adding category error");
                }
                return Ok();
            }
            return BadRequest("Adding category error");
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var operationResult = await _productsService.EditProduct(product);
                if (!operationResult.IsSuccess)
                {
                    _logger.LogInformation($"An error occurred while editing a product:\n{operationResult.Message}");
                    return BadRequest("Editing category error");
                }
                product.Images = product.Images.Where(i => i.Path != null).ToList();
                foreach (var i in product.Images)
                {
                    var editImageOperationResult = await _imagesService.EditImage(i);
                    if (!editImageOperationResult.IsSuccess)
                    {
                        _logger.LogInformation($"An error occurred while editing an image:\n{operationResult.Message}");
                        return BadRequest("Editing category error");
                    }
                }
                return Ok();
            }
            return BadRequest("Editing category error");
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Hide/{id}")]
        public async Task<ActionResult> Hide(int id)
        {
            var operationResult = await _productsService.DeleteProductById(id);
            if (!operationResult.IsSuccess)
            {
                _logger.LogInformation($"An error occurred while hiding a product:\n{operationResult.Message}");
                return BadRequest("Hiding product error");
            }
            return Ok();
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            var operationResult = await _productsService.RestoreProductById(id);
            if (!operationResult.IsSuccess)
            {
                _logger.LogInformation($"An error occurred while restoring a product:\n{operationResult.Message}");
                return BadRequest("Restoring product error");
            }
            return Ok();
        }

        [Authorize(Roles = UserRolesConstants.AdminRole, AuthenticationSchemes = "Bearer")]
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var operationResult = await _productsService.HardDeleteProductById(id);
            if (!operationResult.IsSuccess)
            {
                _logger.LogInformation($"An error occurred while deleting a product:\n{operationResult.Message}");
                return BadRequest("Deleting product error");
            }
            return Ok();
        }
    }
}
