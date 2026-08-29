using Application.Abstraction.Repository;

namespace Application.Feature.User.AddUser;

public static class AddUserHandler
{
    public static async Task HandleAsync(
        AddUserCommand command, 
        IUserRepository userRepository
        )
    {
        if (await userRepository.GetUserByEmail(command.Email) is {})
            throw new Exception("User already exists");
        
        await userRepository.Add(command);
        
    }
}
