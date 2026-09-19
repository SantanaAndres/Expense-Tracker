namespace Application.Abstraction.Services;

public interface ITokenService
{
    public string GenerateToken(string username);
}