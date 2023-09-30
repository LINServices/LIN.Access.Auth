namespace LIN.Access.Auth.Controllers;


public class Authentication
{


    /// <summary>
    /// Inicia una sesión
    /// </summary>
    public static async Task<ReadOneResponse<AccountModel>> Login(string cuenta, string password, string? app = null)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("application", $"{app ?? Build.Application}");

        // ApiServer de la solicitud GET
        var url = ApiServer.PathURL("authentication/login");

        // Armar la url
        url = Web.AddParameters(url, new()
        {
            {
                "user", cuenta
            },
            {
                "password", password
            }
        });


        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

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
    /// Inicia una sesión
    /// </summary>
    public static async Task<ReadOneResponse<AccountModel>> Login(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        var url = ApiServer.PathURL("authentication/LoginWithToken");


        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Add("token", $"{token}");

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



}