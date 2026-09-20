using Application.Abstraction.Repository;

namespace Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository: IRefreshTokenRepository
{
    public Task AddAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public Task RevokeAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}