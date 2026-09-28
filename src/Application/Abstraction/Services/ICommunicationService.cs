namespace Application.Abstraction.Services;

public interface ICommunicationService
{
    Task SendMessage(string toEmail, string toName, string subject, string htmlContent);
}