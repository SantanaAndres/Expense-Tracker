using Application.Abstraction.Repository;
using Application.Abstraction.Services;
using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response.User;
using Application.Helper.Exceptions;
using InvalidCredentialException = Application.Helper.Exceptions.InvalidCredentialException;

namespace Application.Feature.User.Get;

public record GetUserByEmailPasswordQuery(string Email, string Password);
public class GetUserByEmailPasswordQueryHandler(
    ITokenService tokenService , 
    IUserRepository userRepository, 
    IRefreshTokenRepository refreshTokenRepository
    )
{
    public async Task<LoginResponse> HandleAsync(GetUserByEmailPasswordQuery query, CancellationToken cancellationToken)
    {
        var user =  await userRepository.CheckUSerEmailAndPassword(query.Email, cancellationToken);
        
        if (user == null)
            throw new NotFoundException("Email not found");
        
        bool isValid = BCrypt.Net.BCrypt.Verify(query.Password, user.Password);
        
        if (!isValid)
            throw new InvalidCredentialException("Invalid password");
        
        GenerateUserTokenDto generateUserTokenDto = new GenerateUserTokenDto(user.UserId, user.Email);
        
        var token = tokenService.GenerateToken(generateUserTokenDto);
        
        var refreshToken = tokenService.GenerateRefreshToken();
        
        await refreshTokenRepository.AddAsync(new AddRefreshTokenDto(refreshToken, user.UserId), cancellationToken);

        return new LoginResponse(
            token,
            refreshToken
            );
    }
}