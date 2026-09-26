using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IUserRepository
{
    Task<User> GetUserById(int userId, CancellationToken cancellationToken);
    
    Task<User> AddAsync(User user, CancellationToken cancellationToken);
    
    Task<User?> CheckUserExistence(string email, string phoneNumber, CancellationToken cancellationToken);
    
    Task<int> ModifyUserPassword(int id, string password, CancellationToken cancellationToken);
    
    Task<User> CheckUSerEmailAndPassword(string email, CancellationToken cancellationToken);
}