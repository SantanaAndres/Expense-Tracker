using Application.Abstraction.Repository;

namespace Application.Feature.User.Add;

public static class AddUserHandler
{
    public static async Task HandleAsync(
        AddUserCommand command, 
        IUserRepository userRepository
        )
    {
        var userCheck = await userRepository.CheckUserExistence(command.Email, command.PhonerNumber);
        if (userCheck != null)
            throw new Exception("User already exists");
        
        await userRepository.AddAsync(command);
    }
}
