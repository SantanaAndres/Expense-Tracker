using Application.Abstraction.Repository;
using Application.Dto;
using Application.Feature.User.AddUser;
using Application.Feature.User.UpdatePasswordUser;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<User> Add(AddUserCommand user) => throw new NotImplementedException("UserRepository.Add");

    public async Task<User?> GetUserByEmail(string email) => null;

    public async Task<User> ModifyUserPassword(ModifyUserPasswordDto user) => throw new NotImplementedException("UserRepository.ModifyUserPassword");
}
