using Microsoft.EntityFrameworkCore;
using product_api.Data;
using product_api.Models;
using product_api.Repository.IRepository;

namespace product_api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<User> GetAsync(string email)
        {
            return await _db.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
        public async Task AddAsync(User entity)
        {
            _db.Users.Add(entity);
            await SaveAsync();
        }
        public User? Authenticate(UserLogin userLogin)
        {
            return _db.Users.FirstOrDefault(x=>x.Email == userLogin.Email&&
                                            x.Password==userLogin.Password);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
