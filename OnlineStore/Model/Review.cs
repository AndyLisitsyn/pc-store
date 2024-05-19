using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string DesiredDisplayName { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
