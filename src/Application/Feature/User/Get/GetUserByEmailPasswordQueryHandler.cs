using Application.Abstraction.Repository;
using Application.Abstraction.Services;
using Application.Dto;
using Application.Dto.Request;
using Application.Dto.Response.User;

namespace Application.Feature.User.Get;

public record GetUserByEmailPasswordQuery(string Email, string Password);
public class GetUserByEmailPasswordQueryHandler(
    ITokenService tokenService , 
    IUserRepository userRepository, 
    IRefreshTokenService refreshTokenService, 
    IRefreshTokenRepository refreshTokenRepository
    )
{
    public async Task<LoginResponse> HandleAsync(GetUserByEmailPasswordQuery query, CancellationToken cancellationToken)
    {
        var user =  await userRepository.CheckUSerEmailAndPassword(query.Email, cancellationToken);
        
        if (user == null)
            throw new Exception("Invalid email");
        
        bool isValid = BCrypt.Net.BCrypt.Verify(query.Password, user.Password);
        
        if (!isValid)
            throw new Exception("Invalid password");
        
        GenerateUserTokenDto generateUserTokenDto = new GenerateUserTokenDto(user.UserId, user.Email);
        
        var token = tokenService.GenerateToken(generateUserTokenDto);
        
        var refreshToken = refreshTokenService.GenerateRefreshToken();
        
        await refreshTokenRepository.AddAsync(new AddRefreshTokenDto(refreshToken, user.UserId), cancellationToken);

        return new LoginResponse(
            token,
            refreshToken,
            (DateTime.Now.AddDays(30) - DateTime.Now).TotalSeconds
            );
    }
}