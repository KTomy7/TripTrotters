using System.Security.Claims;

namespace TripTrotters;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }

    public static bool IsLoggedIn(this ClaimsPrincipal user)
    {
        return user.Identity!.IsAuthenticated;
    }
}