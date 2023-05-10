using Microsoft.EntityFrameworkCore;
using product_api.Data;
using product_api.Models;
using product_api.Repository.IRepository;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace product_api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Product entity)
        {
            _db.Products.Add(entity);
            await SaveAsync();
        }
        public async Task<Product> GetAsync(int id)
        {
            return await _db.Products.SingleOrDefaultAsync(x => x.Id == id);
        }

        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }
        public List<Product> GetPublished()
        {
            return   _db.Products.Where(x => x.State == "Published").ToList();
        }
        public List<Product> GetDrafts()
        {
            return _db.Products.Where(x => x.State == "Draft").ToList();
        }
        public List<Product> GetModerated()
        {
            return _db.Products.Where(x => x.State == "Moderate").ToList();
        }
        public Task<Product> GetAsync(Expression<Func<Product, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Product entity)
        {
            _db.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            entity.Updated_at = DateTime.Now;
            _db.Products.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
