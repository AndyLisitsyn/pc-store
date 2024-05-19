using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Product
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.0, 1000000000)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Code { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual List<Image> Images { get; set; } = new List<Image>();
    }
}
