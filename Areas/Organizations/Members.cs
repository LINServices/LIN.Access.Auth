namespace LIN.Access.Auth.Areas.Organizations;


public class Members
{


    /// <summary>
    /// Crea un integrante en una organización.
    /// </summary>
    /// <param name="modelo">Modelo del integrante</param>
    /// <param name="token">Token del administrador</param>
    /// <param name="rol">Rol del nuevo integrante</param>
    public static async Task<CreateResponse> Create(AccountModel modelo, string token, OrgRoles rol)
    {

        // Obtiene el cliente http.
        HttpClient client = Service.GetClient("orgs/members/create");

        // Headers.
        client.DefaultRequestHeaders.Add("token", token);
        client.DefaultRequestHeaders.Add("rol", $"{(int)rol}");

        // Serializar el objeto.
        var json = JsonSerializer.Serialize(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PostAsync("", content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<CreateResponse>(responseContent);

            return obj ?? new(Responses.UnavailableService);

        }
        catch (Exception)
        {
        }

        return new(Responses.NotConnection);

    }



    /// <summary>
    /// Obtiene los integrantes asociados a su organización.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    public static async Task<ReadAllResponse<AccountModel>> ReadAll(string token)
    {

        // Obtiene el cliente http.
        HttpClient client = Service.GetClient("orgs/members");

        // Headers.
        client.DefaultRequestHeaders.Add("token", token);

        try
        {

            // Hacer la solicitud GET
            var response = await client.GetAsync("");

            // Leer la respuesta como una cadena
            var responseBody = await response.Content.ReadAsStringAsync();


            var obj = JsonSerializer.Deserialize<ReadAllResponse<AccountModel>>(responseBody);

            return obj ?? new(Responses.UnavailableService);

        }
        catch (Exception)
        {
        }

        return new(Responses.NotConnection);
    }



}