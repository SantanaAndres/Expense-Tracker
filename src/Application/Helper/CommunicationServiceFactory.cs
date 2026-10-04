using Application.Abstraction.Services;
using Application.Helper.Exceptions;

namespace Application.Helper;

public class CommunicationServiceFactory(IEnumerable<ICommunicationService> communicationServices)
{
    public ICommunicationService GetCommunicationService(string communicationServiceType)
    {
        List<string> communicationTypesList = new() { "Sms", "Email" };
        
        if(string.IsNullOrEmpty(communicationServiceType))
            throw new ArgumentNullException("Please provide a valid communication service type");
        
        if(!communicationTypesList.Contains(communicationServiceType))
            throw new NotFoundException("Please provide a valid communication service type");
        
        return communicationServices.FirstOrDefault(x => x.CommunicationServiceType == communicationServiceType);
    }
}