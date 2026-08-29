namespace Application.Feature.User.AddUser;

public static class AddUserHandler
{
    public static Task HandleAsync(AddUserCommand command)
    {
        return Task.CompletedTask;
    }
}
