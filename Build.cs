using Microsoft.Extensions.DependencyInjection;

namespace LIN.Access.Auth;

public static class Build
{

    internal static string Application { get; set; } = string.Empty;


    /// <summary>
    /// Utilizar LIN Authentication.
    /// </summary>
    /// <param name="app">Aplicación.</param>
    /// <param name="url">Ruta.</param>
    public static IServiceCollection AddAuthenticationService(this IServiceCollection service, string? url = null)
    {
        Service._Service = new();
        Service._Service.SetDefault(url ?? "https://api.identity.linplatform.com/");
        return service;
    }
}