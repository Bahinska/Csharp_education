using product_api.Models;

namespace product_api.Repository.IRepository
{
    public interface IProductRepository 
    {
        Task<Product> UpdateAsync(Product entity);
        List<Product> GetAllProducts();
        Task<Product> GetAsync(int id);
        Task CreateAsync(Product entity);
        Task RemoveAsync(Product entity);
        List<Product> GetPublished();
        List<Product> GetModerated();
        List<Product> GetDrafts();
    }
}
