using Application.Abstraction.Repository;

namespace Application.Feature.RefreshToken;

public record RefreshTokenCommand(string Token);

public class RefreshTokenHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository
    )
{
    public async Task HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var refreshToken = await refreshTokenRepository.GetTokenAsync(command.Token, cancellationToken);

        if (refreshToken == null)
            throw new UnauthorizedAccessException("Refresh token not found");
        
        if(refreshToken.IsRevoked)
            throw new UnauthorizedAccessException("Refresh token revoked");
        
        var user = await userRepository.GetUserById(refreshToken.UserId, cancellationToken);
    }
}