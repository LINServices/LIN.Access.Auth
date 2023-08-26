namespace LIN.Access.Auth.Areas.Organizations;


public class Members
{


    /// <summary>
    /// Crea un integrante en una organización.
    /// </summary>
    /// <param name="modelo">Modelo del integrante</param>
    /// <param name="token">Token del administrador</param>
    /// <param name="rol">Rol del nuevo integrante</param>
    public async static Task<CreateResponse> Create(AccountModel modelo, string token, OrgRoles rol)
    {

        // Variables
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("token", token);
        client.DefaultRequestHeaders.Add("rol", $"{(int)rol}");

        string url = ApiServer.PathURL("orgs/members/create");
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

            return obj ?? new(Responses.UnavailableService);

        }
        catch
        {
        }

        return new(Responses.NotConnection);

    }



    /// <summary>
    /// Obtiene los integrantes asociados a su organización.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    public async static Task<ReadAllResponse<AccountModel>> ReadAll(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        httpClient.DefaultRequestHeaders.Add("token", $"{token}");
        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("orgs/members");


        try
        {

            // Hacer la solicitud GET
            HttpResponseMessage response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonConvert.DeserializeObject<ReadAllResponse<AccountModel>>(responseBody);

            return obj ?? new(Responses.UnavailableService);

        }
        catch
        {
        }

        return new(Responses.NotConnection);
    }



}
