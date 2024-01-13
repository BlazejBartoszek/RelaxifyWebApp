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
        public ProductController(ICategoryRepository dbC, IProductRepository dbP)
        {
            _categoryRepo = dbC;
            _productRepo = dbP;
        }       

        public IActionResult Index()
        {
            List<Product> objCategoryList = _productRepo.GetAll().ToList();            

            return View(objCategoryList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _categoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            ViewBag.CategoryList = CategoryList;

            if (id==null || id==0)
            {
                return View(CategoryList);
            }
            else
            {                
                return View(_productRepo.Get(u => u.Id == id));
            }
        }

        [HttpPost]
        public IActionResult Upsert(Product obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Add(obj);
                _productRepo.Save();
                TempData["success"] = "Nowa kategoria została utworzona pomyślnie";

                return RedirectToAction("Index");
            }

            return View();
        }               

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? categoryFromDb = _productRepo.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? categoryFromDb = _productRepo.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _productRepo.Remove(categoryFromDb);
            _productRepo.Save();
            TempData["success"] = "Kategoria została usunięta pomyślnie";

            return RedirectToAction("Index");
        }
    }
}