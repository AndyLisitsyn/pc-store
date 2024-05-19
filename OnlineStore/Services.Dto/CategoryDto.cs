using System.ComponentModel.DataAnnotations;

namespace Services.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
