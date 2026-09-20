namespace Application.Dto;

public record AddRefreshTokenDto(
    string Token,
    int UserId
    );