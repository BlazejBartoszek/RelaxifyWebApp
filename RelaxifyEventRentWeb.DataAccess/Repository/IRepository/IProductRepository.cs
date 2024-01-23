using RelaxifyEventRentWeb.Models;

namespace RelaxifyEventRentWeb.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        void Save();
        IEnumerable<Product> GetOneCategory(int? categoryId);
    }
}