using System.Collections.Generic;
using Services.Dto;

namespace WEB.ViewModels.ProductViewModules
{
    public class ProductsViewModel
    {
        public int CategoryId { get; set; }
        public List<ProductDto> Products { get; set; }
        public int PageNumber { get; set; }
        public int PagesCount { get; set; }
    }
}
