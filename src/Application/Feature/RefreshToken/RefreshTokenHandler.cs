using Application.Abstraction.Repository;
using Application.Abstraction.Services;
using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response.RefreshToken;
using Application.Helper.Exceptions;

namespace Application.Feature.RefreshToken;

public record RefreshTokenCommand(string Token);

public class RefreshTokenHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    ITokenService tokenService
    )
{
    public async Task<RefreshTokenResponse> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var refreshToken = await refreshTokenRepository.GetTokenAsync(command.Token, cancellationToken);

        if (refreshToken == null)
            throw new NotFoundException("Refresh token not found");
        
        if(refreshToken.IsRevoked)
            throw new UnauthorizedAccessException("Refresh token revoked");
        
        if(refreshToken.ExpiryDate < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token expired");
        
        var user = await userRepository.GetUserById(refreshToken.UserId, cancellationToken);
        
        if (user == null)
            throw new NotFoundException("User not found");
        
        await refreshTokenRepository.RevokeAsync(refreshToken.Id, cancellationToken);
        
        var newRefreshToken = tokenService.GenerateRefreshToken();
        
        GenerateUserTokenDto generateUserTokenDto = new(user.UserId, user.Email);
        
        var accessToken = tokenService.GenerateToken(generateUserTokenDto);
        
        AddRefreshTokenDto refreshTokenDto = new(newRefreshToken ,refreshToken.UserId);
        
        var refreshResult = await refreshTokenRepository.AddAsync(refreshTokenDto, cancellationToken);
        
        return new RefreshTokenResponse(
            accessToken, 
            refreshTokenDto.Token,
            (DateTime.Now - refreshResult.ExpiryDate).TotalSeconds 
            );
        
    }
}