using Application.Dto;
using Domain.Entities;

namespace Application.Abstraction.Repository;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> AddAsync(AddRefreshTokenDto refreshTokenDto, CancellationToken cancellationToken);
    
    Task<RefreshToken> RevokeAsync(int id, CancellationToken cancellationToken);
    
}