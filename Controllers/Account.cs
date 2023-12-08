namespace LIN.Access.Auth.Controllers;


public static class Account
{


    /// <summary>
    /// Crear nuevo usuario.
    /// </summary>
    /// <param name="modelo">Modelo.</param>
    public static async Task<CreateResponse> Create(AccountModel modelo)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("account/create");

        // Resultado.
        var Content= await client.Post<CreateResponse>(modelo);

        // Retornar.
        return Content;

    }



    /// <summary>
    /// Obtiene una cuenta según el ID.
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
        client.AddParameter(new()
        {
           {"id", $"{id}"}
        });

        // Get.
        var (Content, _) = await client.Get<ReadOneResponse<AccountModel>>();

        return Content;

    }



    /// <summary>
    /// Obtiene una lista de cuentas según los ID.
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
        client.AddParameter(new()
        {
           {"user", $"{cuenta}"}
        });

        // Get.
        var (Content, _) = await client.Get<ReadOneResponse<AccountModel>>();

        return Content;

    }



    /// <summary>
    /// Buscar usuario por coincidencia del usuario.
    /// </summary>
    /// <param name="pattern">Patron de búsqueda.</param>
    /// <param name="token">Token de acceso.</param>
    /// <param name="isAdmin">Es un administrador.</param>
    public static async Task<ReadAllResponse<AccountModel>> Search(string pattern, string token, bool isAdmin)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        string url;
        if (isAdmin)
        {
            url = Service.PathURL("account/admin/search");
        }
        else
        {
            url = Service.PathURL("account/search");
        }

        url = Web.AddParameters(url, new()
        {
            {
                "pattern", pattern
            }
        });

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("token", $"{token}");




        try
        {
            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ReadAllResponse<AccountModel>>(responseBody) ?? new();


            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    /// <summary>
    /// Obtiene los datos de una cuenta especifica
    /// </summary>
    /// <param name="id">ID de la cuenta</param>
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

















    ///// <summary>
    ///// Actualizar la contraseña de una cuenta
    ///// </summary>
    ///// <param name="modelo">Modelo de actualización</param>
    //public static async Task<ResponseBase> UpdatePassword(UpdatePasswordModel modelo, string token)
    //{

    //    // Obtiene el cliente http.
    //    HttpClient client = Service.GetClient("account/update/password");

    //    // Headers.
    //    client.DefaultRequestHeaders.Add("token", token);

    //    var json = JsonSerializer.Serialize(modelo);

    //    try
    //    {
    //        // Contenido
    //        StringContent content = new(json, Encoding.UTF8, "application/json");

    //        // Envía la solicitud
    //        var response = await client.PatchAsync("", content);

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



    /// <summary>
    /// Desactiva una cuenta
    /// </summary>
    /// <param name="id">ID de la cuenta</param>
    /// <param name="password">Contraseña</param>
    public static async Task<ResponseBase> Disable(int id, string password)
    {

        // Variables
        var client = new HttpClient();

        var url = Service.PathURL("account/disable");
        var json = JsonSerializer.Serialize(new AccountModel()
        {
            ID = id,
            Contraseña = password
        });

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Actualiza el genero de una cuenta
    /// </summary>
    /// <param name="token">Token de acceso</param>
    /// <param name="genero">Nuevo genero</param>
    public static async Task<ResponseBase> UpdateGender(string token, Genders genero)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("token", $"{token}");
        client.DefaultRequestHeaders.Add("genero", $"{(int)genero}");

        var url = Service.PathURL("account/update/gender");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Actualiza la visibilidad de una cuenta
    /// </summary>
    /// <param name="token">Token de acceso</param>
    /// <param name="visibility">Nueva visibilidad</param>
    public static async Task<ResponseBase> UpdateVisibility(string token, AccountVisibility visibility)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("token", $"{token}");
        client.DefaultRequestHeaders.Add("visibility", $"{(int)visibility}");

        var url = Service.PathURL("account/update/visibility");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }































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





    /// <summary>
    /// Actualiza la informacion de un usuario
    /// </summary>
    public static async Task<ReadOneResponse<EmailModel>> ForgetPassword(string user)
    {

        // Variables
        var client = new HttpClient();

        var url = Service.PathURL("security/password/forget");

        url = Web.AddParameters(url, new()
        {
            {
                "user", user
            }
        });

        try
        {
            HttpRequestMessage ms = new(HttpMethod.Post, url);

            // Envía la solicitud
            var response = await client.SendAsync(ms);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ReadOneResponse<EmailModel>>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }













    public static async Task<ResponseBase> Verficate(string key)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("key", key);

        var url = Service.PathURL("security/mails/verify");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }





















    public static async Task<ReadAllResponse<LoginLogModel>> LoginLogs(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        var url = Service.PathURL("account/logs/read/all");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);



        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonSerializer.Deserialize<ReadAllResponse<LoginLogModel>>(responseBody);

            return obj ?? new();


        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



















}