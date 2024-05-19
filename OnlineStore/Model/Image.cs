using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
        public int Index { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
