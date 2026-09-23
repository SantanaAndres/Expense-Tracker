using System.Security.Claims;

namespace ExpenseTrackerApi.Extension;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue("UserId");
        
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User ID not found in claims.");
        
        return int.Parse(userId);
        
    }
}