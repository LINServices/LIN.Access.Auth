namespace LIN.Access.Auth.Controllers;


public class Applications
{




    public static async Task<ReadAllResponse<ApplicationModel>> ReadAll(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        var url = ApiServer.PathURL("applications");

       

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<ApplicationModel>>(responseBody);

            return obj ?? new();


        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }



}
