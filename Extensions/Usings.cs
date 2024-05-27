using LIN.Access.Auth.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LIN.Access.Auth.Extensions;


public static class Usings
{


    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddSingleton<IISession, SessionAuth>();
        return services;
    }


}