using Application.Abstraction.Repository;
using Application.Abstraction.Services;
using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response.RefreshToken;

namespace Application.Feature.RefreshToken;

public record RefreshTokenCommand(string Token);

public class RefreshTokenHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    IRefreshTokenService refreshTokenService,
    ITokenService tokenService
    )
{
    public async Task<RefreshTokenResponse> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var refreshToken = await refreshTokenRepository.GetTokenAsync(command.Token, cancellationToken);

        if (refreshToken == null)
            throw new UnauthorizedAccessException("Refresh token not found");
        
        if(refreshToken.IsRevoked)
            throw new UnauthorizedAccessException("Refresh token revoked");
        
        var user = await userRepository.GetUserById(refreshToken.UserId, cancellationToken);
        
        if (user == null)
            throw new UnauthorizedAccessException("User not found");
        
        await refreshTokenRepository.RevokeAsync(refreshToken.Id, cancellationToken);
        
        var newRefreshToken = refreshTokenService.GenerateRefreshToken();
        
        GenerateUserTokenDto generateUserTokenDto = new(user.UserId, user.Email);
        
        var accessToken = tokenService.GenerateToken(generateUserTokenDto);
        
        AddRefreshTokenDto refreshTokenDto = new(accessToken ,refreshToken.UserId);
        
        var refreshResult = await refreshTokenRepository.AddAsync(refreshTokenDto, cancellationToken);
        
        return new RefreshTokenResponse(
            accessToken, 
            refreshTokenDto.Token,
            (DateTime.Now - refreshResult.ExpiryDate).Seconds
            );
        
    }
}