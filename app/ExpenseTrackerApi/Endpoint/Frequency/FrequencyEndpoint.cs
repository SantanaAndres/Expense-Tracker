using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Wolverine.Http;

namespace ExpenseTrackerApi.Endpoint.Frequency;

public static class FrequencyEndpoint
{
    [WolverineGet("/get-frequency")]
    [Tags("FrequencyEnum")]
    [EndpointSummary("Get all allowed frequencies")]
    [EndpointDescription("Endpoint that give to the user all the allowed frequencies")]
    [Authorize]
    public static List<string> GetAllAllowedFrequencies()
    {
        return Enum.GetNames<FrequencyEnum>().ToList();
    }
}