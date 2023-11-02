using VetCare.API.Identification.Domain.Models;

namespace VetCare.API.Identification.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    Task<User> FindByUsernameAsync(string email);
    public bool ExistsByUsername(string email);
    User FindById(int id);
    void Update(User user);
    void Remove(User user);
    
}