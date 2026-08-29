using Application.Dto;
using Application.Feature.User.AddUser;
using Application.Feature.User.UpdatePasswordUser;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IUserRepository
{
    Task<User> Add(AddUserCommand user);
    
    Task<User?> GetUserByEmail(string email);

    Task<User> ModifyUserPassword(ModifyUserPasswordDto user);
}