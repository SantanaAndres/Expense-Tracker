using Application.Abstraction.Repository;
using Application.Feature.User.Add;
using Application.Feature.User.UpdatePasswordUser;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(ExpenseTrackerDbContext dbContext) : IUserRepository
{
    public async Task<User> GetUserById(int userId)
    {
        return await dbContext.Users.Where(user => user.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task<User> AddAsync(AddUserCommand user)
    {
        var userResponse = await dbContext.Users.AddAsync(new ()
        {
            Email = user.Email,
            Password = user.Password,
            PhoneNumber = user.PhonerNumber
        });
        await dbContext.SaveChangesAsync();
        
        return userResponse.Entity;
    }

    public async Task<User?> CheckUserExistence(string email, string phoneNumber)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email || user.PhoneNumber == phoneNumber);
    }

    public async Task<User> ModifyUserPassword(ModifyUserPasswordCommand user) => throw new NotImplementedException("UserRepository.ModifyUserPassword");
}
