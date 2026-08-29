using Application.Abstraction.Repository;
using Application.Dto;
using Application.Feature.User.AddUser;
using Application.Feature.User.UpdatePasswordUser;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public Task<User> Add(AddUserCommand user) => throw new NotImplementedException("UserRepository.Add");

    public Task<User> ModifyUserPassword(ModifyUserPasswordDto user) => throw new NotImplementedException("UserRepository.ModifyUserPassword");
}
