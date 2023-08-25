namespace LIN.Access.Auth.Controllers;


public static class Account
{


    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    public async static Task<CreateResponse> Create(AccountModel modelo)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("account/create");
        string json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<CreateResponse>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Obtiene los datos de una cuenta especifica
    /// </summary>
    /// <param name="id">ID de la cuenta</param>
    public async static Task<ReadOneResponse<AccountModel>> Read(int id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("account/read/id");

        url = Web.AddParameters(url, new(){
            {"id",id.ToString() }
        });

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);



        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadOneResponse<AccountModel>>(responseBody);

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
    public async static Task<ReadAllResponse<AccountModel>> Read(List<int> id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("account/find");


        var json = JsonConvert.SerializeObject(id);

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new StringContent(json, Encoding.UTF8, "application/json");



        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.PostAsync(url, request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<AccountModel>>(responseBody);

            return obj ?? new();


        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



    /// <summary>
    /// Obtiene los datos de una cuenta
    /// </summary>
    /// <param name="cuenta">Usuario de la cuenta</param>
    public async static Task<ReadOneResponse<AccountModel>> Read(string cuenta)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("account/read/user");

        url = Web.AddParameters(url, new(){
            {"user", cuenta }
        });


        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        try
        {
            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);


            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadOneResponse<AccountModel>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    /// <summary>
    /// Buscar usuarios por medio de un patron
    /// </summary>
    /// <param name="pattern">Patron</param>
    /// <param name="id">ID de context</param>
    public async static Task<ReadAllResponse<AccountModel>> Search(string pattern, string token, bool isAdmin)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        string url;
        if (isAdmin)
        {
            url = ApiServer.PathURL("account/admin/search");
        }
        else
        {
            url = ApiServer.PathURL("account/search");
        }

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("token", $"{token}");


        url = Web.AddParameters(url, new()
        {
            {"pattern", pattern }
        });

        try
        {
            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadAllResponse<AccountModel>>(responseBody) ?? new();


            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    /// <summary>
    /// Actualizar la contraseña de una cuenta
    /// </summary>
    /// <param name="modelo">Modelo de actualización</param>
    public async static Task<ResponseBase> UpdatePassword(UpdatePasswordModel modelo, string token)
    {

        // Variables
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("token", token);

        string url = ApiServer.PathURL("account/update/password");
        string json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Desactiva una cuenta
    /// </summary>
    /// <param name="id">ID de la cuenta</param>
    /// <param name="password">Contraseña</param>
    public async static Task<ResponseBase> Disable(int id, string password)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("account/disable");
        string json = JsonConvert.SerializeObject(new AccountModel()
        {
            ID = id,
            Contraseña = password
        });

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

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
    public async static Task<ResponseBase> UpdateGender(string token, Genders genero)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("token", $"{token}");
        client.DefaultRequestHeaders.Add("genero", $"{(int)genero}");

        string url = ApiServer.PathURL("account/update/gender");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

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
    public async static Task<ResponseBase> UpdateVisibility(string token, AccountVisibility visibility)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("token", $"{token}");
        client.DefaultRequestHeaders.Add("visibility", $"{(int)visibility}");

        string url = ApiServer.PathURL("account/update/visibility");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }































    public async static Task<ResponseBase> ResetPassword(string key, UpdatePasswordModel modelo)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("key", key);

        string url = ApiServer.PathURL("security/password/reset");
        string json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }





    /// <summary>
    /// Actualiza la informacion de un usuario
    /// </summary>
    public async static Task<ReadOneResponse<EmailModel>> ForgetPassword(string user)
    {

        // Variables
        var client = new HttpClient();

        string url = ApiServer.PathURL("security/password/forget");

        url = Web.AddParameters(url, new()
        {
            {"user",user }
        });

        try
        {
            HttpRequestMessage ms = new(HttpMethod.Post, url);

            // Envía la solicitud
            HttpResponseMessage response = await client.SendAsync(ms);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<EmailModel>>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }













    public async static Task<ResponseBase> Verficate(string key)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("key", key);

        string url = ApiServer.PathURL("security/mails/verify");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }





















    public async static Task<ReadAllResponse<LoginLogModel>> LoginLogs(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("account/logs/read/all");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);



        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<LoginLogModel>>(responseBody);

            return obj ?? new();


        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



















}
