using Application.Dto.Request;

namespace Application.Abstraction.Services;

public interface ITokenService
{
    public string GenerateToken(GenerateUserTokenDto user);
}