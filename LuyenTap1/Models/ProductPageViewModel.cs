using System.Reflection.Metadata;

namespace LuyenTap1.Models
{
    public class ProductPageViewModel
    {
        public List<ProductViewModel> Products = new List<ProductViewModel>();
        public string? searchString { get; set; }
        public string? sortOrder { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
    }
}
