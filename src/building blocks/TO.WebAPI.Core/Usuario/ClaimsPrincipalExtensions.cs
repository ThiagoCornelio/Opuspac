using System.Security.Claims;

namespace TO.WebAPI.Core.Usuario;

public static class ClaimsPrincipalExtensions // Apenas para facilitar a consulta do usuário na Controller
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException(nameof(principal));
        }

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        return claim?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException(nameof(principal));
        }

        var claim = principal.FindFirst("email");
        return claim?.Value;
    }

    public static string GetUserToken(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException(nameof(principal));
        }

        var claim = principal.FindFirst("JWT");
        return claim?.Value;
    }
}