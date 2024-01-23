using RelaxifyEventRentWeb.DataAccess.Data;
using RelaxifyEventRentWeb.DataAccess.Repository.IRepository;
using RelaxifyEventRentWeb.Models;

namespace RelaxifyEventRentWeb.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Product.FirstOrDefault(u => u.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.Price = obj.Price;
                objFromDb.ProductName = obj.ProductName;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;

                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }

        public IEnumerable<Product> GetOneCategory(int? categoryId)
        {
            return _db.Product.Where(x => x.CategoryId == categoryId).ToList();
        }
    }
}
