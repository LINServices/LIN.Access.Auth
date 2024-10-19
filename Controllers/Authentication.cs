namespace LIN.Access.Auth.Controllers;

public class Authentication
{

    /// <summary>
    /// Iniciar sesión.
    /// </summary>
    /// <param name="cuenta">Usuario.</param>
    /// <param name="password">Contraseña.</param>
    /// <param name="app">Aplicación.</param>
    public static async Task<ReadOneResponse<AccountModel>> Login(string cuenta, string password, string? app = null)
    {

        // Cliente.
        Client client = Service.GetClient("authentication/login");

        client.TimeOut = 20;

        // Headers.
        client.AddHeader("application", $"{app ?? Build.Application}");

        // Parámetros
        client.AddParameter("user", cuenta);
        client.AddParameter("password", password);

        var response = await client.Get<ReadOneResponse<AccountModel>>();

        return response;

    }


    /// <summary>
    /// Inicia una sesión
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<AccountModel>> Login(string token)
    {

        // Cliente.
        Client client = Service.GetClient("authentication/LoginWithToken");

        client.TimeOut = 20;

        // Headers.
        client.AddHeader("token", $"{token}");

        var response = await client.Get<ReadOneResponse<AccountModel>>();

        return response;

    }



    public static async Task<ReadOneResponse<AccountModel>> OnPolicy(string user, string password, string policy)
    {

        // Cliente.
        Client client = Service.GetClient("authentication/validate/policy");

        client.TimeOut = 20;

        // Headers.
        client.AddHeader("policy", $"{policy}");

        client.AddParameter("user", $"{user}");
        client.AddParameter("password", $"{password}");

        var response = await client.Get<ReadOneResponse<AccountModel>>();

        return response;

    }

}