using Services.Dto;
using System.Collections.Generic;
using Model;

namespace WEB.ViewModels.ProductViewModules
{
    public class NewProductViewModel
    {
        public int CategoryId { get; set; }
        public int PageNumber { get; set; }
        public ProductDto Product { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
