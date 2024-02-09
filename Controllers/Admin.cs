namespace LIN.Access.Auth.Controllers;


public static class Admin
{



    /// <summary>
    /// Obtiene los datos de una cuenta especifica
    /// </summary>
    /// <param name="id">Id de la cuenta</param>
    public static async Task<ReadOneResponse<AccountModel>> Read(int id, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        var url = Service.PathURL("administrator/read/id");

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


            var obj = JsonSerializer.Deserialize<ReadOneResponse<AccountModel>>(responseBody);

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
    public static async Task<ReadOneResponse<AccountModel>> Read(string cuenta, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        var url = Service.PathURL("administrator/read/user");

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


            var obj = JsonSerializer.Deserialize<ReadOneResponse<AccountModel>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



}