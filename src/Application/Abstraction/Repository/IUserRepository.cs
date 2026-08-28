using Application.Dto;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IUserRepository
{
    User Add(AddUserDto user);
}