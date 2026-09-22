using Application.Dto.Request;

namespace Application.Abstraction.Services;

public interface ITokenService
{
    string GenerateToken(GenerateUserTokenDto user);
    
    string GenerateRefreshToken();

}