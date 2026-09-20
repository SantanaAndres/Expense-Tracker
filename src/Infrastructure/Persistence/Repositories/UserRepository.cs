using Application.Abstraction.Repository;
using Application.Feature.User.Add;
using Application.Feature.User.UpdatePasswordUser;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(ExpenseTrackerDbContext dbContext) : IUserRepository
{
    public async Task<User> GetUserById(int userId, CancellationToken cancellationToken)
    {
        return await dbContext.Users.Where(user => user.UserId == userId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User> CheckUSerEmailAndPassword(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users.Where(user => user.Email == email).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User> AddAsync(User newUser, CancellationToken cancellationToken)
    {
        var userResponse = await dbContext.Users.AddAsync(newUser, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return userResponse.Entity;
    }

    public async Task<User?> CheckUserExistence(string email, string phoneNumber, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email || user.PhoneNumber == phoneNumber, cancellationToken);
    }

    public async Task<int> ModifyUserPassword(int userId, string password, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .Where(u => u.UserId == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(u => u.Password, password), cancellationToken);
    }
}
