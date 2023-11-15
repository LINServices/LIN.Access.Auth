namespace LIN.Access.Auth.Areas.Organizations;


public static class Organizations
{


    /// <summary>
    /// Crea una nueva organización.
    /// </summary>
    /// <param name="organization">Modelo de la organización</param>
    /// <param name="admin">Usuario administrador</param>
    public static async Task<CreateResponse> Create(OrganizationModel organization, AccountModel admin)
    {

        // Variables
        var client = new HttpClient();


        organization.AppList = new();
        organization.Members = new()
        {
            new()
            {
                Member = admin,
                Rol = OrgRoles.SuperManager
            }
        };
        admin.OrganizationAccess = null;

        var url = ApiServer.PathURL("orgs/create");
        var json = JsonSerializer.Serialize(organization);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<CreateResponse>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }



    /// <summary>
    /// Actualiza es estado de la lista blanca de una organización.
    /// </summary>
    /// <param name="token">Token de administrador</param>
    /// <param name="estado">Nuevo estado</param>
    public static async Task<ResponseBase> UpdateWhiteListState(string token, bool estado)
    {

        // Variables
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("token", token);

        var url = ApiServer.PathURL("orgs/update/whitelist");


        url = Web.AddParameters(url, new()
        {
            {
                "haveWhite", $"{estado}"
            }
        });

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
    /// Actualiza es estado del acceso login de los integrantes de una organización
    /// </summary>
    /// <param name="token">Token de administrador</param>
    /// <param name="estado">Nuevo estado</param>
    public static async Task<ResponseBase> UpdateAccessState(string token, bool estado)
    {

        // Variables
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("token", token);

        var url = ApiServer.PathURL("orgs/update/access");


        url = Web.AddParameters(url, new()
        {
            {
                "state", $"{estado}"
            }
        });

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



}