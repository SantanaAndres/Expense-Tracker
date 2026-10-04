using Application.Abstraction.Repository;
using Application.Helper.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Feature.User.UpdatePasswordUser;

public record ModifyUserPasswordCommand(int Id, string Password);

public class ModifyUserPasswordHandler(
    IUserRepository userRepository,
    ILogger<ModifyUserPasswordHandler> logger)
{
    public async Task<ModifyUserPasswordResponse> HandleAsync(ModifyUserPasswordCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Modifying user with id {command.Id}");
     
        var userId = await userRepository.GetUserById(command.Id, cancellationToken);

        if (userId is null)
        {
            logger.LogError($"User with id {command.Id} not found");
            throw new NotFoundException("User not found");
        }
           
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);
        
        await userRepository.ModifyUserPassword(command.Id, hashPassword, cancellationToken);

        return new ModifyUserPasswordResponse(userId.UserId, command.Password);
    }
}

public record ModifyUserPasswordResponse(int Id, string NewPassword);