using System.Collections.Generic;
using Services.Dto;

namespace WEB.ViewModels.CategoryViewModels
{
    public class CategoriesViewModel
    {
        public List<CategoryDto> ChildCategories { get; set; }
        public CategoryDto Category { get; set; }
    }
}
