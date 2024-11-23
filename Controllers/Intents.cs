namespace LIN.Access.Auth.Controllers;

public static class Intents
{

    /// <summary>
    /// Obtiene la lista de intentos passkey.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<PassKeyModel>> ReadAll(string token)
    {

        // Cliente.
        Client client = Service.GetClient("Intents");

        // Headers.
        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Get<ReadAllResponse<PassKeyModel>>();

        return response;
    }


    /// <summary>
    /// Obtiene la lista de intentos passkey.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<int>> Count(string token)
    {

        // Cliente.
        Client client = Service.GetClient("Intents/count");

        // Headers.
        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Get<ReadOneResponse<int>>();

        return response;
    }

}