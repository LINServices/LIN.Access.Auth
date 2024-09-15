using Microsoft.AspNetCore.Builder;

namespace LIN.Access.Auth;

public static class Build
{

    internal static string Application { get; set; } = string.Empty;


    /// <summary>
    /// Utilizar LIN Authentication.
    /// </summary>
    /// <param name="app">Aplicación.</param>
    /// <param name="url">Ruta.</param>
    public static IApplicationBuilder UseAuthentication(this IApplicationBuilder app, string?url)
    {
        Service._Service = new();
        Service._Service.SetDefault(url ?? "https://api.identity.linplatform.com/");
        return app;
    }
}