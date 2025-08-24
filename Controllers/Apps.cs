namespace LIN.Access.Auth.Controllers;

public class Apps
{

   
    public static async Task<ResponseBase> Request(string key)
    {

        // Cliente.
        Client client = Service.GetClient("applications/token");

        // Headers.
        client.AddHeader("key", key);
        // Respuesta
        var response = await client.Get<ResponseBase>();

        return response;

    }

    public static async Task<ReadOneResponse<ApplicationModel>> Information(string token)
    {

        // Cliente.
        Client client = Service.GetClient("applications/information");

        // Headers.
        client.AddHeader("token", token);
        // Respuesta
        var response = await client.Get<ReadOneResponse<ApplicationModel>>();

        return response;

    }



}