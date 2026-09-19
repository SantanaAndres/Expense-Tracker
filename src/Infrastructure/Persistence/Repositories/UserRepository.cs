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

    public async Task<User> CheckUSerEmailAndPassword(string email, string password)
    {
        return await dbContext.Users.Where(user => user.Email == email && user.Password == password).FirstOrDefaultAsync();
    }

    public async Task<User> AddAsync(User newUser)
    {
        var userResponse = await dbContext.Users.AddAsync(newUser);
        await dbContext.SaveChangesAsync();
        
        return userResponse.Entity;
    }

    public async Task<User?> CheckUserExistence(string email, string phoneNumber)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email || user.PhoneNumber == phoneNumber);
    }

    public async Task<User> ModifyUserPassword(ModifyUserPasswordCommand user) => throw new NotImplementedException("UserRepository.ModifyUserPassword");
}
