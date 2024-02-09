namespace LIN.Access.Auth.Controllers;


//public class Applications
//{


//    /// <summary>
//    /// Crear aplicación.
//    /// </summary>
//    /// <param name="application">Modelo.</param>
//    /// <param name="token">Token.</param>
//    public static async Task<CreateResponse> Create(ApplicationModel application, string token)
//    {

//        // Cliente.
//        Client client = Service.GetClient("applications");

//        // Headers.
//        client.DefaultRequestHeaders.Add("token", token);

//        // Respuesta.
//        var response = await client.Post<CreateResponse>(application);

//        return response;

//    }



//    ///// <summary>
//    ///// Obtiene las aplicaciones asociadas a una cuenta.
//    ///// </summary>
//    ///// <param name="token">Token de acceso.</param>
//    //public static async Task<ReadAllResponse<ApplicationModel>> ReadAll(string token)
//    //{

//    //    // Cliente.
//    //    Client client = Service.GetClient("applications");

//    //    // Headers.
//    //    client.DefaultRequestHeaders.Add("token", token);

//    //    // Respuesta.
//    //    var response = await client.Get<ReadAllResponse<ApplicationModel>>();

//    //    return response;

//    //}



//    /// <summary>
//    /// Insertar acceso al directorio de una app.
//    /// </summary>
//    /// <param name="appId">Id de la app.</param>
//    /// <param name="accountId">Id de la cuenta.</param>
//    /// <param name="token">Token de acceso</param>
//    public static async Task<ReadOneResponse<bool>> AllowTo(int appId, int accountId, string token)
//    {

//        // Cliente.
//        Client client = Service.GetClient("applications");

//        // Headers.
//        client.DefaultRequestHeaders.Add("token", token);
//        client.DefaultRequestHeaders.Add("appId", appId.ToString());
//        client.DefaultRequestHeaders.Add("accountId", accountId.ToString());

//        // Respuesta.
//        var response = await client.Put<ReadOneResponse<bool>>();

//        return response;

//    }



//}