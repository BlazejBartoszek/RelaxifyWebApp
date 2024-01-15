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
            List<Product> objCategoryList = _productRepo.GetAll().ToList();

            TempData["ImageUrl"] = "\\images\\product\\1a5c43f3-dc8a-4413-8c0b-1ee05905a0fd.jpg";

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
                TempData["UpsertTitle"] = "Utwórz";
                TempData["UpsertConfirmButton"] = "Utwórz";
                TempData["ImageUrl"] = "https://as1.ftcdn.net/v2/jpg/02/57/42/72/1000_F_257427286_Lp7c9XdPnvN46TyFKqUaZpPADJ77ZzUk.jpg";
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

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    
                    productVM.ImageUrl = @"\images\product\" + fileName;                    
                }                

                _productRepo.Add(productVM);
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