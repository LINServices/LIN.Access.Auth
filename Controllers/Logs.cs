namespace LIN.Access.Auth.Controllers;


//public class Logs
//{


//    /// <summary>
//    /// Obtiene la lista de accesos asociados a una cuenta de usuario.
//    /// </summary>
//    /// <param name="token">Token de acceso.</param>
//    public static async Task<ReadAllResponse<LoginLogModel>> LoginLogs(string token)
//    {

//        // Cliente.
//        Client client = Service.GetClient("account/logs");

//        // Headers.
//        client.AddHeader("token", token);

//        // Respuesta
//        var response = await client.Get<ReadAllResponse<LoginLogModel>>();

//        return response;

//    }


//}