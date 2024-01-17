using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RelaxifyEventRentWeb.DataAccess.Repository.IRepository;
using RelaxifyEventRentWeb.Models;

namespace RelaxifyEventRentWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ICategoryRepository dbC, IProductRepository dbP, IWebHostEnvironment webHostEnvironment)
        {
            _categoryRepo = dbC;
            _productRepo = dbP;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _productRepo.GetAll(includeProperties: "Category").ToList();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            ViewBag.CategoryList = CategoryList;

            if (id == null || id == 0)
            {
                TempData["UpsertTitle"] = "Utwórz";
                TempData["UpsertConfirmButton"] = "Utwórz";
                TempData["ImageUrl"] = "https://placehold.co/500x600";
                return View();
            }
            else
            {
                Product productRepo = _productRepo.Get(u => u.Id == id);
                TempData["UpsertTitle"] = "Edytuj";
                TempData["UpsertConfirmButton"] = "Zapisz";
                TempData["ImageUrl"] = productRepo.ImageUrl;

                return View(productRepo);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Product productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.ImageUrl = @"\images\product\" + fileName;
                }                

                if (productVM.Id == 0)
                {
                    if (file == null)
                    {
                        productVM.ImageUrl = "https://placehold.co/500x600";
                    }
                    
                    _productRepo.Add(productVM);
                    TempData["success"] = "Nowa kategoria została utworzona pomyślnie";
                }
                else
                {
                    _productRepo.Update(productVM);
                    TempData["success"] = "Nowa kategoria została zmodyfikowana pomyślnie";
                }

                _productRepo.Save();


                return RedirectToAction("Index");
            }

            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _productRepo.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product productToBeDeleted = _productRepo.Get(u => u.Id == id);

            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Wystąpił problem podczas usuwania" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _productRepo.Remove(productToBeDeleted);
            _productRepo.Save();

            List<Product> objProductList = _productRepo.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = objProductList });
        }
        #endregion
    }
}