namespace LIN.Access.Auth.Areas.Organizations;


public static class Organizations
{


    /// <summary>
    /// Crea una nueva organización.
    /// </summary>
    /// <param name="organization">Modelo de la organización</param>
    /// <param name="admin">Usuario administrador</param>
    public static async Task<CreateResponse> Create(OrganizationModel organization, AccountModel admin, string token)
    {

        // Obtiene el cliente http.
        HttpClient client = Service.GetClient("orgs/create");

        // Headers.
        client.DefaultRequestHeaders.Add("token", token);


        // Organizar el modelo.
        admin.OrganizationAccess = null;
        organization.Members = new()
        {
            new()
            {
                Member = admin,
                Rol = OrgRoles.SuperManager
            }
        };

        // Serializar el objeto.
        var json = JsonSerializer.Serialize(organization);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await client.PostAsync("", content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<CreateResponse>(responseContent);

            return obj ?? new();

        }
        catch (Exception)
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

        // Cliente HTTP.
        Client client = Service.GetClient("orgs/update/whitelist");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter(new()
        {
           {"haveWhite", $"{estado}"}
        });

        // Get.
        var (Content, _) = await client.Get<ReadOneResponse<AccountModel>>();

        return Content;

    }



    /// <summary>
    /// Actualiza es estado del acceso login de los integrantes de una organización
    /// </summary>
    /// <param name="token">Token de administrador</param>
    /// <param name="estado">Nuevo estado</param>
    public static async Task<ResponseBase> UpdateAccessState(string token, bool estado)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("orgs/update/access");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter(new()
        {
           {"state", $"{estado}"}
        });

        // Get.
        var (Content, _) = await client.Get<ResponseBase>();

        return Content;

    }



}