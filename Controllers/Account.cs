namespace LIN.Access.Auth.Controllers;


public static class Account
{


    /// <summary>
    /// Crear una cuenta.
    /// </summary>
    /// <param name="modelo">Modelo.</param>
    public static async Task<CreateResponse> Create(AccountModel modelo)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("account/create");

        // Resultado.
        var Content = await client.Post<CreateResponse>(modelo);

        // Retornar.
        return Content;

    }



    /// <summary>
    /// Obtiene una cuenta según el Id.
    /// </summary>
    /// <param name="id">Id del usuario.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<AccountModel>> Read(int id, string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("account/read/id");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter("id", id);


        // Get.
        var content = await client.Get<ReadOneResponse<AccountModel>>();

        return content;

    }






    /// <summary>
    /// Obtiene una cuenta según el Id.
    /// </summary>
    /// <param name="id">Id del usuario.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<AccountModel>> ReadByIdentity(int id, string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("account/read/identity");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter("id", id);

        // Get.
        var content = await client.Get<ReadOneResponse<AccountModel>>();

        return content;

    }



    /// <summary>
    /// Obtiene una cuenta según el usuario único.
    /// </summary>
    /// <param name="cuenta">Usuario de la cuenta.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<AccountModel>> Read(string cuenta, string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("account/read/user");

        // Headers.
        client.AddHeader("token", token);

       // Parámetros.
        client.AddParameter("user", cuenta);

        // Get.
        var content = await client.Get<ReadOneResponse<AccountModel>>();

        return content;

    }



    /// <summary>
    /// Obtiene una lista de cuentas según los Id.
    /// </summary>
    /// <param name="ids">Lista de Ids.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<AccountModel>> Read(List<int> ids, string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("account/findAll");

        // Headers.
        client.AddHeader("token", token);

        // Get.
        var Content = await client.Post<ReadAllResponse<AccountModel>>(ids);

        return Content;

    }



    /// <summary>
    /// Buscar usuarios por coincidencia del usuario.
    /// </summary>
    /// <param name="pattern">Patron de búsqueda.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<AccountModel>> Search(string pattern, string token)
    {

        // Cliente.
        Client client = Service.GetClient("account/search");

        // Parámetros.
        client.AddParameter("pattern", $"{pattern}");

        // Headers.
        client.AddHeader("token", token);

        // Response.
        var content = await client.Get<ReadAllResponse<AccountModel>>();

        return content;

    }






    /// <summary>
    /// Obtiene los datos de una cuenta especifica.
    /// </summary>
    /// <param name="id">Id de la cuenta</param>
    public static async Task<ReadOneResponse<AccountModel>> ReadAdmin(int id, string token)
    {

        var x = await Read(new List<int>
        {
id
        }, token);


        return new()
        {
Message = x.Message,
Model = x.Models.FirstOrDefault() ?? new(),
Response = x.Response
        };

    }



    /// <summary>
    /// Actualiza el genero de una cuenta.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    /// <param name="genero">Nuevo genero</param>
    public static async Task<ResponseBase> UpdateGender(string token, Genders genero)
    {

        // Cliente.
        Client client = Service.GetClient("account/update/gender");

        // Headers.
        client.AddHeader("token", $"{token}");
        client.AddHeader("visibility", $"{(int)genero}");

        var response = await client.Patch<ResponseBase>();

        return response;

    }



    /// <summary>
    /// Actualiza la visibilidad de una cuenta.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    /// <param name="visibility">Nueva visibilidad</param>
    //public static async Task<ResponseBase> UpdateVisibility(string token, AccountVisibility visibility)
    //{

    //    // Cliente.
    //    Client client = Service.GetClient("account/update/visibility");

    //    // Headers.
    //    client.AddHeader("token", $"{token}");
    //    client.AddHeader("visibility", $"{(int)visibility}");

    //    var response = await client.Patch<ResponseBase>();

    //    return response;

    //}



    /// <summary>
    /// Actualizar la contraseña.
    /// </summary>
    /// <param name="account">Id de la cuenta.</param>
    /// <param name="oldPassword">Contraseña antigua.</param>
    /// <param name="newPassword">Contraseña nueva.</param>
    public static async Task<ResponseBase> UpdatePassword(int account, string oldPassword, string newPassword)
    {

        // Obtiene el cliente http.
        Client client = Service.GetClient("account/security/update/password");

        // Parámetros.
        client.AddParameter("actualPassword", oldPassword);
        client.AddParameter("newPassword", newPassword);

        // Headers.
        client.AddHeader("account", account.ToString());

        var content = await client.Patch<ResponseBase>();

        return content ?? new();

    }



    /// <summary>
    /// Eliminar (Desactivar) una cuenta.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ResponseBase> Delete(string token)
    {

        // Obtiene el cliente http.
        Client client = Service.GetClient("account/security/delete");

        // Headers.
        client.AddHeader("token", token);

        var content = await client.Delete<ResponseBase>();

        return content ?? new();

    }







































    //======================== NO MAPEADOS ==========================//











    /// <summary>
    /// Desactiva una cuenta
    /// </summary>
    /// <param name="id">Id de la cuenta</param>

    //public static async Task<ResponseBase> Disable(int id, string password)
    //{

    //    // Variables
    //    var client = new HttpClient();

    //    var url = Service.PathURL("account/disable");
    //    var json = JsonSerializer.Serialize(new AccountModel()
    //    {
    //        Id = id,
    //        Contraseña = password
    //    });

    //    try
    //    {
    //        // Contenido
    //        StringContent content = new(json, Encoding.UTF8, "application/json");

    //        // Envía la solicitud
    //        var response = await client.PatchAsync(url, content);

    //        // Lee la respuesta del servidor
    //        var responseContent = await response.Content.ReadAsStringAsync();

    //        var obj = JsonSerializer.Deserialize<ResponseBase>(responseContent);

    //        return obj ?? new();

    //    }
    //    catch
    //    {
    //    }

    //    return new();

    //}

































    //public static async Task<ResponseBase> ResetPassword(string key, UpdatePasswordModel modelo)
    //{

    //    // Variables
    //    var client = new HttpClient();
    //    client.DefaultRequestHeaders.Add("key", key);

    //    var url = Service.PathURL("security/password/reset");
    //    var json = JsonSerializer.Serialize(modelo);

    //    try
    //    {
    //        // Contenido
    //        StringContent content = new(json, Encoding.UTF8, "application/json");

    //        // Envía la solicitud
    //        var response = await client.PatchAsync(url, content);

    //        // Lee la respuesta del servidor
    //        var responseContent = await response.Content.ReadAsStringAsync();

    //        var obj = JsonSerializer.Deserialize<ResponseBase>(responseContent);

    //        return obj ?? new();

    //    }
    //    catch
    //    {
    //    }

    //    return new();

    //}





    ///// <summary>
    ///// Actualiza la informacion de un usuario
    ///// </summary>
    //[Obsolete]
    //public static async Task<ReadOneResponse<EmailModel>> ForgetPassword(string user)
    //{

    //    // Variables
    //    var client = new HttpClient();

    //    var url = Service.PathURL("security/password/forget");

    //    url = Web.AddParameters(url, new()
    //    {
    //        {
    //"user", user
    //        }
    //    });

    //    try
    //    {
    //        HttpRequestMessage ms = new(HttpMethod.Post, url);

    //        // Envía la solicitud
    //        var response = await client.SendAsync(ms);

    //        // Lee la respuesta del servidor
    //        var responseContent = await response.Content.ReadAsStringAsync();

    //        var obj = JsonSerializer.Deserialize<ReadOneResponse<EmailModel>>(responseContent);

    //        return obj ?? new();

    //    }
    //    catch
    //    {
    //    }

    //    return new();

    //}












    //[Obsolete]
    //public static async Task<ResponseBase> Verficate(string key)
    //{

    //    // Variables
    //    var client = new HttpClient();
    //    client.DefaultRequestHeaders.Add("key", key);

    //    var url = Service.PathURL("security/mails/verify");

    //    try
    //    {
    //        // Contenido
    //        StringContent content = new("", Encoding.UTF8, "application/json");

    //        // Envía la solicitud
    //        var response = await client.PostAsync(url, content);

    //        // Lee la respuesta del servidor
    //        var responseContent = await response.Content.ReadAsStringAsync();

    //        var obj = JsonSerializer.Deserialize<ResponseBase>(responseContent);

    //        return obj ?? new();

    //    }
    //    catch
    //    {
    //    }

    //    return new();

    //}































}

