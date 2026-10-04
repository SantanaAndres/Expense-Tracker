using Application.Abstraction.Repository;

namespace Application.Feature.User.Add;

public record AddUserCommand(string Email, string Password, string PhonerNumber);

public static class AddUserHandler
{
    public static async Task HandleAsync(
        AddUserCommand command, 
        IUserRepository userRepository,
        CancellationToken cancellationToken
        )
    {
        Domain.Entities.User? userCheckByEmail = await userRepository.CheckUserExistenceByEmail(command.Email, cancellationToken);
        
        Domain.Entities.User? userCheckByPhone = await userRepository.CheckUserExistenceByPhone(command.PhonerNumber, cancellationToken);
        
        if (userCheckByEmail != null || userCheckByPhone != null)
            throw new Exception("User already exists");
        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

        Domain.Entities.User newUser = new()
        {
            Email = command.Email,
            Password = passwordHash,
            PhoneNumber = command.PhonerNumber
        };
        
        await userRepository.AddAsync(newUser, cancellationToken);
    }
}
