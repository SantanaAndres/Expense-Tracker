namespace Application.Dto.Response.User;

public record LoginResponse(
    string AccessToken,
    string RefreshToken
    );