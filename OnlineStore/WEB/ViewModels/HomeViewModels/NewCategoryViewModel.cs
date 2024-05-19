using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Dto;

namespace WEB.ViewModels.HomeViewModels
{
    public class NewCategoryViewModel
    {
        public CategoryDto Category { get; set; }
        public List<CategoryDto> ParentCategories { get; set; }
    }
}
