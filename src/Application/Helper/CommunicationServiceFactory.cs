using Application.Abstraction.Services;

namespace Application.Helper;

public class CommunicationServiceFactory(IEnumerable<ICommunicationService> communicationServices)
{
    public ICommunicationService GetCommunicationService(string communicationServiceType)
    {
        return communicationServices.FirstOrDefault(x => x.CommunicationServiceType == communicationServiceType);
    }
}