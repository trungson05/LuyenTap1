using System.ComponentModel.DataAnnotations;

namespace LuyenTap1.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        public List<Product> Products = new List<Product>();
    }
}
