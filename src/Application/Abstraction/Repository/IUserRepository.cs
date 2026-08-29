using Application.Dto;
using Application.Feature.User.AddUser;
using Application.Feature.User.UpdatePasswordUser;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IUserRepository
{
    Task<User> Add(AddUserDto user);

    Task<User> ModifyUserPassword(ModifyUserPasswordDto user);
}