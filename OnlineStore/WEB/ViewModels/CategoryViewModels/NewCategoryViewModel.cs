using System.Collections.Generic;
using Services.Dto;

namespace WEB.ViewModels.CategoryViewModels
{
    public class NewCategoryViewModel
    {
        public int ParentCategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public List<CategoryDto> ParentCategories { get; set; }
    }
}
