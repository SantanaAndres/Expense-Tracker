namespace Application.Abstraction.Repository;

public interface IRefreshTokenRepository
{
    Task AddAsync(CancellationToken cancellationToken);
    
    Task RevokeAsync(CancellationToken cancellationToken);
    
}