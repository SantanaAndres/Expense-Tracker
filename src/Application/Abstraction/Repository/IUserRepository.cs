using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IUserRepository
{
    Task<User> GetUserById(int userId, CancellationToken cancellationToken);
    
    Task<User> AddAsync(User user, CancellationToken cancellationToken);
    
    Task<User?> CheckUserExistenceByEmail(string email, CancellationToken cancellationToken);
    
    Task<User?> CheckUserExistenceByPhone(string phone, CancellationToken cancellationToken);
    
    Task<int> ModifyUserPassword(int id, string password, CancellationToken cancellationToken);
    
    Task<User> CheckUSerEmailAndPassword(string email, CancellationToken cancellationToken);
}