using Microsoft.AspNetCore.Mvc;
using RelaxifyEventRentWeb.DataAccess.Repository.IRepository;
using RelaxifyEventRentWeb.Models;
using System.Diagnostics;

namespace RelaxifyEventRentWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ICategoryRepository dbC, IProductRepository dbP)
        {
            _categoryRepo = dbC;
            _productRepo = dbP;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productListFromDb = _productRepo.GetAll(includeProperties: "Category");

            return View(productListFromDb);
        }

        public IActionResult Details(int productId)
        {
            Product product = _productRepo.Get(u => u.Id == productId, includeProperties: "Category");
            return View(product);
        }

        [HttpGet]
        public IActionResult GetOneCategory(int? categoryId)
        {
            IEnumerable<Product> OneCategoryProductList = _productRepo.GetOneCategory(categoryId);

            return View("Index", OneCategoryProductList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
