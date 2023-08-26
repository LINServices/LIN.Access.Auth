namespace LIN.Access.Auth.Areas.Organizations;


public class Applications
{


    /// <summary>
    /// Obtiene la lista de aplicaciones permitidas en tu organización.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    public async static Task<ReadAllResponse<ApplicationModel>> ReadAll(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        httpClient.DefaultRequestHeaders.Add("token", $"{token}");
        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("orgs/applications");


        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<ApplicationModel>>(responseBody);

            return obj ?? new(Responses.UnavailableService);


        }
        catch
        {
        }


        return new(Responses.NotConnection);
    }



    /// <summary>
    /// Obtiene la lista de aplicación según un parámetro que no coincidan con la de una organización.
    /// </summary>
    /// <param name="param">Parámetro de búsqueda</param>
    /// <param name="token">Token de acceso</param>
    public async static Task<ReadAllResponse<ApplicationModel>> Search(string param, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("orgs/applications/search");


        url = Web.AddParameters(url, new()
        {
            {"param", param }
        });

        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<ApplicationModel>>(responseBody);

            return obj ?? new(Responses.UnavailableService);


        }
        catch
        {
        }


        return new(Responses.NotConnection);
    }



}
