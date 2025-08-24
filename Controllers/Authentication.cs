namespace LIN.Access.Auth.Controllers;

public class Authentication
{

    /// <summary>
    /// Iniciar sesión.
    /// </summary>
    /// <param name="cuenta">Usuario.</param>
    /// <param name="password">Contraseña.</param>
    public static async Task<ReadOneResponse<AccountModel>> Login(string cuenta, string password)
    {
        // Cliente.
        Client client = Service.GetClient("authentication/login");

        client.TimeOut = 20;

        // Headers.
        client.AddHeader("application", $"{Build.Application}");

        // Parámetros
        client.AddParameter("user", cuenta);
        client.AddParameter("password", password);

        var response = await client.Get<ReadOneResponse<AccountModel>>();

        return response;
    }


    /// <summary>
    /// Iniciar sesión.
    /// </summary>
    /// <param name="cuenta">Usuario.</param>
    /// <param name="password">Contraseña.</param>
    /// <param name="app">Aplicación.</param>
    public static async Task<ReadOneResponse<AccountModel>> LoginV4(string cuenta, string password, string token)
    {

        // Cliente.
        Client client = Service.GetClient("v4/authenticationV4/login");

        client.TimeOut = 20;

        // Headers.
        client.AddHeader("token", token);

        // Parámetros
        client.AddParameter("user", cuenta);
        client.AddParameter("password", password);

        var response = await client.Get<ReadOneResponse<AccountModel>>();

        return response;
    }


    /// <summary>
    /// Iniciar sesión.
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


    /// <summary>
    /// Validar cuenta con usuario, contraseña y política.
    /// </summary>
    /// <param name="user">Usuario.</param>
    /// <param name="password">Contraseña.</param>
    /// <param name="policy">Política.</param>
    public static async Task<ReadOneResponse<AccountModel>> OnPolicy(string user, string password, string policy)
    {

        // Cliente.
        Client client = Service.GetClient("authentication/validate/policy");

        client.TimeOut = 20;

        // Headers.
        client.AddHeader("policy", $"{policy}");

        // Parámetros.
        client.AddParameter("user", $"{user}");
        client.AddParameter("password", $"{password}");

        var response = await client.Get<ReadOneResponse<AccountModel>>();

        return response;

    }

}