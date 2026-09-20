using Application.Dto;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetTokenAsync(string token, CancellationToken cancellationToken);
    
    Task<RefreshToken> AddAsync(AddRefreshTokenDto refreshTokenDto, CancellationToken cancellationToken);
    
    Task<RefreshToken> RevokeAsync(int id, CancellationToken cancellationToken);
    
}