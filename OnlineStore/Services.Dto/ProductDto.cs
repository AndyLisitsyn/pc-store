using Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int Code { get; set; }
        public int CategoryId { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
    }
}
