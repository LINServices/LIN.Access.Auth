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
        client.DefaultRequestHeaders.Add("token", token);

        // Respuesta.
        var response = await client.Get<ReadAllResponse<PassKeyModel>>();

        return response;
    }


}