using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuyenTap1.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        [Required]
        [StringLength(100000)]
        public string? ProductDescription { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? category;

    }
}
