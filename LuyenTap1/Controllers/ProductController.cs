using LuyenTap1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LuyenTap1.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public async Task<ActionResult> Index(string searchString, string sortOrder,int pageNumber =1,int pageSize=3)
        {
            var product = (from p in Data.SeedData.Products
                          join c in Data.SeedData.Categories on p.CategoryId equals c.CategoryId
                          select new ProductViewModel
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              ProductPrice = p.ProductPrice,
                              ProductDescription = p.ProductDescription,
                              CategoryName = c.CategoryName
                          }).ToList();
            if (!string.IsNullOrEmpty(searchString)){
                string keyword = searchString.ToLower();
                product = product.Where(x => (x.ProductName != null && x.ProductName.ToLower().Contains(keyword))
                || (x.CategoryName != null && x.CategoryName.ToLower().Contains(keyword))).ToList();
            }
            ViewBag.fillterName = searchString;
            if(sortOrder == "name_tang")
            {
                product = product.OrderBy(x => x.ProductName).ToList();
            }else if (sortOrder == "name_giam")
            {
                product = product.OrderByDescending(x => x.ProductName).ToList();
            }else if (sortOrder == "price_tang")
            {
                product = product.OrderBy(x => x.ProductPrice).ToList();
            }
            else if(sortOrder=="price_giam")
            {
                product = product.OrderByDescending(x => x.ProductPrice).ToList();
            }
            var totalItem = product.Count();
            var totalPages = (int)Math.Ceiling((double)totalItem / pageSize);
            var productsInpage = product.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var viewModel = new ProductPageViewModel
            {
                Products = productsInpage,
                currentPage = pageNumber,
                totalPages = totalPages,
                searchString = searchString,
                sortOrder = sortOrder
              

            };
            return View(viewModel);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            var category = Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            var detail = new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                CategoryName = category?.CategoryName
            };
            return View(detail);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                newProduct.ProductId = Data.SeedData.Products.Max(p => p.ProductId+1);
                Data.SeedData.Products.Add(newProduct);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", newProduct.CategoryId);
            return View(newProduct);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var editProduct = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (editProduct == null)
                {
                    return NotFound();
                }
                editProduct.ProductName = product.ProductName;
                editProduct.ProductPrice = product.ProductPrice;
                editProduct.ProductDescription = product.ProductDescription;
                editProduct.CategoryId = product.CategoryId;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(Data.SeedData.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            var category = Data.SeedData.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            ViewBag.CategoryName = category?.CategoryName;
           
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var deleteProduct = Data.SeedData.Products.FirstOrDefault(p => p.ProductId == id);
            if (deleteProduct == null)
            {
                return NotFound();
            }
            Data.SeedData.Products.Remove(deleteProduct);
            return RedirectToAction(nameof(Index));
        }
    }
}
