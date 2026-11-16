using LuyenTap1.Models;

namespace LuyenTap1.Data
{
    public class SeedData
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category{CategoryId=1,CategoryName="Dien thoai" },
            new Category{CategoryId=2,CategoryName="Laptop" },
            new Category{CategoryId=3,CategoryName="PC" }
        };
        public static List<Product> Products = new List<Product>
        {
            new Product{ProductId=1,ProductName="Dell Latitude 3410",ProductPrice=6900000,ProductDescription="Laptop Dell",CategoryId=2},
            new Product{ProductId=2,ProductName="Mac",ProductPrice=60000000,ProductDescription="PC Mac",CategoryId=3},
            new Product{ProductId=3,ProductName="Iphone 18 promax",ProductPrice=39000000,ProductDescription="Iphone 18 promax",CategoryId=1}

        };
    }
}
