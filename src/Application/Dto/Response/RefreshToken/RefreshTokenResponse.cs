namespace Application.Dto.Response.RefreshToken;

public record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken,
    double ExpiresIn
);