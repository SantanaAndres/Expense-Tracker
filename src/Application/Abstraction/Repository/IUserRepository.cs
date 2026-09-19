using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IUserRepository
{
    Task<User> GetUserById(int userId);
    
    Task<User> AddAsync(User user);
    
    Task<User?> CheckUserExistence(string email, string phoneNumber);
    
    Task<int> ModifyUserPassword(int id, string password);
    
    Task<User> CheckUSerEmailAndPassword(string email, string password);
}