using Application.Dto;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IUserRepository
{
    Task<User> Add(AddUserDto user);
}