﻿namespace LIN.Access.Auth.Controllers;


public static class Account
{


    /// <summary>
    /// Crear nuevo usuario.
    /// </summary>
    /// <param name="modelo">Modelo.</param>
    public static async Task<CreateResponse> Create(AccountModel modelo)
    {

        // Variables
        var client = new HttpClient();

        var url = ApiServer.PathURL("account/create");
        var json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<CreateResponse>(responseContent);

            return obj ?? new();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new();

    }



    /// <summary>
    /// Obtiene una cuenta según el Id.
    /// </summary>
    /// <param name="id">Id del usuario.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<AccountModel>> Read(int id, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        var url = ApiServer.PathURL("account/read/id");

        url = Web.AddParameters(url, new()
        {
            {
                "id", id.ToString()
            }
        });

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);



        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();


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
    /// Obtiene una lista de cuentas según los Id.
    /// </summary>
    /// <param name="ids">Lista de Ids.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<AccountModel>> Read(List<int> ids, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        var url = ApiServer.PathURL("account/findAll");

        var json = JsonConvert.SerializeObject(ids);

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.PostAsync(url, request);

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();

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
    /// Obtiene una cuenta según el usuario único.
    /// </summary>
    /// <param name="cuenta">Usuario de la cuenta.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<AccountModel>> Read(string cuenta, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        var url = ApiServer.PathURL("account/read/user");

        url = Web.AddParameters(url, new()
        {
            {
                "user", cuenta
            }
        });


        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        try
        {
            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);


            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();


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
            url = ApiServer.PathURL("account/admin/search");
        }
        else
        {
            url = ApiServer.PathURL("account/search");
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

















    /// <summary>
    /// Actualizar la contraseña de una cuenta
    /// </summary>
    /// <param name="modelo">Modelo de actualización</param>
    public static async Task<ResponseBase> UpdatePassword(UpdatePasswordModel modelo, string token)
    {

        // Variables
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("token", token);

        var url = ApiServer.PathURL("account/update/password");
        var json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

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
    public static async Task<ResponseBase> Disable(int id, string password)
    {

        // Variables
        var client = new HttpClient();

        var url = ApiServer.PathURL("account/disable");
        var json = JsonConvert.SerializeObject(new AccountModel()
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
    public static async Task<ResponseBase> UpdateGender(string token, Genders genero)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("token", $"{token}");
        client.DefaultRequestHeaders.Add("genero", $"{(int)genero}");

        var url = ApiServer.PathURL("account/update/gender");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

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
    public static async Task<ResponseBase> UpdateVisibility(string token, AccountVisibility visibility)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("token", $"{token}");
        client.DefaultRequestHeaders.Add("visibility", $"{(int)visibility}");

        var url = ApiServer.PathURL("account/update/visibility");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }































    public static async Task<ResponseBase> ResetPassword(string key, UpdatePasswordModel modelo)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("key", key);

        var url = ApiServer.PathURL("security/password/reset");
        var json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

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
    public static async Task<ReadOneResponse<EmailModel>> ForgetPassword(string user)
    {

        // Variables
        var client = new HttpClient();

        var url = ApiServer.PathURL("security/password/forget");

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

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<EmailModel>>(responseContent);

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

        var url = ApiServer.PathURL("security/mails/verify");

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

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
        var url = ApiServer.PathURL("account/logs/read/all");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);



        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();


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