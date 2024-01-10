using Microsoft.AspNetCore.Mvc;
using RelaxifyEventRentWeb.DataAccess.Data;
using RelaxifyEventRentWeb.Models;

namespace RelaxifyEventRentWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Category.ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Nowa kategoria została utworzona pomyślnie";

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Category.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Kategoria została edytowana pomyślnie";

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

            Category? categoryFromDb = _db.Category.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            Category? categoryFromDb = _db.Category.Find(obj.Id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _db.Category.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["success"] = "Kategoria została usunięta pomyślnie";

            return RedirectToAction("Index");            
        }
    }
}