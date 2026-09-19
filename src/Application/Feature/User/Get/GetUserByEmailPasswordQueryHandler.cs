using Application.Abstraction.Repository;

namespace Application.Feature.User.Get;

public record GetUserByEmailPasswordQuery(string Email, string Password);

public class GetUserByEmailPasswordQueryHandler
{
    public async Task HandleAsync(GetUserByEmailPasswordQuery query, IUserRepository userRepository)
    {
        var user =  await userRepository.CheckUSerEmailAndPassword(query.Email, query.Password);
        
        if (user == null)
            throw new Exception("Invalid email or password");
        
    }
}