using Application.Abstraction.Repository;
using Application.Dto;
using Application.Dto.Response.RefreshToken;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(ExpenseTrackerDbContext dbContext): IRefreshTokenRepository
{
    public async Task<RefreshToken> GetTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await dbContext.RefreshTokens.Where(x => x.Token == token).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<RefreshToken> AddAsync(AddRefreshTokenDto refreshTokenDto, CancellationToken cancellationToken)
    {
        RefreshToken refreshToken = new RefreshToken()
        {
            Token = refreshTokenDto.Token,
            UserId = refreshTokenDto.UserId
        };
        
        var result = await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
    
    public async Task<RefreshToken> RevokeAsync(int id, CancellationToken cancellationToken)
    {
        var refreshToken = await dbContext.RefreshTokens.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        
        refreshToken.IsRevoked = true;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return refreshToken;
    }
}