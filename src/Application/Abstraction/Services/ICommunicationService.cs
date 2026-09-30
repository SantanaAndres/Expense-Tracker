namespace Application.Abstraction.Services;

public interface ICommunicationService
{
    string CommunicationServiceType { get; }
    Task<bool> SendMessage(CommunicationServiceParameters  parameters);
}

public record CommunicationServiceParameters(string Message, string Subject, string To);