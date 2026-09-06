using Application.Feature.User.Add;
using Application.Feature.User.UpdatePasswordUser;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IUserRepository
{
    Task<User> GetUserById(int userId);
    Task<User> AddAsync(AddUserCommand user);
    
    Task<User?> CheckUserExistence(string email, string phoneNumber);

    Task<User> ModifyUserPassword(ModifyUserPasswordCommand user);
}