using Application.Abstraction.Repository;

namespace Application.Feature.User.UpdatePasswordUser;

public record ModifyUserPasswordCommand(int Id, string Password);

public class ModifyUserPasswordHandler
{
    public async Task HandleAsync(ModifyUserPasswordCommand command, IUserRepository userRepository)
    {
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);
        
        await userRepository.ModifyUserPassword(command.Id, hashPassword);
    }
}