using Microsoft.AspNetCore.Mvc;
using RelaxifyEventRentWeb.DataAccess.Repository.IRepository;
using RelaxifyEventRentWeb.Models;

namespace RelaxifyEventRentWeb.ViewComponents
{
    public class MenuCategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepo;
        public MenuCategoryListViewComponent(ICategoryRepository dbC)
        {
            _categoryRepo = dbC;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Category> categoryList = _categoryRepo.GetAll();

            return View(categoryList);
        }
    }
}