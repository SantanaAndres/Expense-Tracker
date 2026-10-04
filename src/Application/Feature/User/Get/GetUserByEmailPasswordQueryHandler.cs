using Application.Abstraction.Repository;
using Application.Abstraction.Services;
using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response.User;
using Application.Helper.Exceptions;
using Microsoft.Extensions.Logging;
using InvalidCredentialException = Application.Helper.Exceptions.InvalidCredentialException;

namespace Application.Feature.User.Get;

public record GetUserByEmailPasswordQuery(string Email, string Password);
public class GetUserByEmailPasswordQueryHandler(
    ITokenService tokenService , 
    IUserRepository userRepository, 
    IRefreshTokenRepository refreshTokenRepository,
    ILogger<GetUserByEmailPasswordQueryHandler> logger)
{
    public async Task<LoginResponse> HandleAsync(GetUserByEmailPasswordQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetUserByEmailPasswordQuery");
        
        var user =  await userRepository.CheckUSerEmailAndPassword(query.Email, cancellationToken);

        if (user == null)
        {
            logger.LogError("User not found for {email}", query.Email);
            throw new NotFoundException("Invalid credentials");
        }
        
        bool isValid = BCrypt.Net.BCrypt.Verify(query.Password, user.Password);

        if (!isValid)
        {
            logger.LogError("User not found for {email} and {password}", query.Email, query.Password);
            throw new InvalidCredentialException("Invalid credentials");
        }

        var amountRevokeTokens = await refreshTokenRepository.RevokeAllTokensByUserAsync(user.UserId, cancellationToken);
        
        logger.LogInformation("Amount of tokens revoked: {amount}", amountRevokeTokens);
        
        GenerateUserTokenDto generateUserTokenDto = new GenerateUserTokenDto(user.UserId, user.Email);
        
        var token = tokenService.GenerateToken(generateUserTokenDto);
        
        var refreshToken = tokenService.GenerateRefreshToken();
        
        await refreshTokenRepository.AddAsync(new AddRefreshTokenDto(refreshToken, user.UserId), cancellationToken);

        logger.LogInformation("Saving new token from user {userId}", user.UserId);
        
        return new LoginResponse(
            token,
            refreshToken
            );
    }
}