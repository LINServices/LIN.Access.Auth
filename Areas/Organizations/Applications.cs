namespace LIN.Access.Auth.Areas.Organizations;


public class Applications
{


    /// <summary>
    /// Crea el acceso permitido a una aplicación en una organización.
    /// </summary>
    /// <param name="uId">Uid de la aplicación</param>
    /// <param name="token">Token del administrador</param>
    public static async Task<CreateResponse> Create(string uId, string token)
    {

        // Obtiene el cliente http.
        HttpClient client = Service.GetClient("orgs/applications/insert", new()
        {
            {"appUid", uId }
        });

        // Headers.
        client.DefaultRequestHeaders.Add("token", token);

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PostAsync("", content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            // Obtiene el objeto.
            var obj = JsonSerializer.Deserialize<CreateResponse>(responseContent);

            return obj ?? new(Responses.UnavailableService);

        }
        catch (Exception)
        {
        }

        return new(Responses.NotConnection);

    }



    /// <summary>
    /// Obtiene la lista de aplicaciones permitidas en tu organización.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    public static async Task<ReadAllResponse<ApplicationModel>> ReadAll(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        httpClient.DefaultRequestHeaders.Add("token", $"{token}");
        // ApiServer de la solicitud GET
        var url = Service.PathURL("orgs/applications");


        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonSerializer.Deserialize<ReadAllResponse<ApplicationModel>>(responseBody);

            return obj ?? new(Responses.UnavailableService);


        }
        catch
        {
        }


        return new(Responses.NotConnection);
    }



    /// <summary>
    /// Obtiene la lista de aplicaciones según un parámetro que no coincidan con la de una organización.
    /// </summary>
    /// <param name="param">Parámetro de búsqueda</param>
    /// <param name="token">Token de acceso</param>
    public static async Task<ReadAllResponse<ApplicationModel>> Search(string param, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        var url = Service.PathURL("orgs/applications/search");


        url = Web.AddParameters(url, new()
        {
            {
                "param", param
            }
        });

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonSerializer.Deserialize<ReadAllResponse<ApplicationModel>>(responseBody);

            return obj ?? new(Responses.UnavailableService);


        }
        catch
        {
        }


        return new(Responses.NotConnection);
    }


}