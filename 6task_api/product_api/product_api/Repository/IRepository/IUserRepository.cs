using product_api.Models;

namespace product_api.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string email);
        Task AddAsync(User entity);
        User? Authenticate(UserLogin userLogin);
        Task SaveAsync();
    }
}
