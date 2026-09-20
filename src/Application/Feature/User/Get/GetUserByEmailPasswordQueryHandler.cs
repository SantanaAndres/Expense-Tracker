using Application.Abstraction.Repository;
using Application.Abstraction.Services;
using Application.Dto.Request;

namespace Application.Feature.User.Get;

public record GetUserByEmailPasswordQuery(string Email, string Password);
public class GetUserByEmailPasswordQueryHandler(ITokenService tokenService , IUserRepository userRepository)
{
    public async Task<string> HandleAsync(GetUserByEmailPasswordQuery query)
    {
        var user =  await userRepository.CheckUSerEmailAndPassword(query.Email);
        
        if (user == null)
            throw new Exception("Invalid email");
        
        bool isValid = BCrypt.Net.BCrypt.Verify(query.Password, user.Password);
        
        if (!isValid)
            throw new Exception("Invalid password");
        
        GenerateUserTokenDto generateUserTokenDto = new GenerateUserTokenDto(user.UserId, user.Email);
        
        var token = tokenService.GenerateToken(generateUserTokenDto);
        
        return token;
        
    }
}