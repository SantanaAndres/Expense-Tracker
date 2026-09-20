namespace Application.Abstraction.Services;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
}