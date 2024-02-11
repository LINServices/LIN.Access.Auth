namespace LIN.Access.Auth.Areas.Organizations;


public static class Organizations
{


    /// <summary>
    /// Crear organización.
    /// </summary>
    /// <param name="organization">Modelo.</param>
    public static async Task<CreateResponse> Create(OrganizationModel organization)
    {

        // Obtiene el cliente http.
        Client client = Service.GetClient("organizations/create");

        var response = await client.Post<CreateResponse>(organization);

        return response;

    }



    /// <summary>
    /// Obtener una organización.
    /// </summary>
    /// <param name="id">Id del organización.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<OrganizationModel>> Read(int id, string token)
    {

        // Obtiene el cliente http.
        Client client = Service.GetClient("organizations/read/id");

        // Headers.
        client.AddHeader("token", token);

        // Consultas.
        client.AddParameter("id", id.ToString());   

        var response = await client.Get<ReadOneResponse<OrganizationModel>>();

        return response;

    }



    /// <summary>
    /// Obtener las organizaciones donde un usuario pertenece.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<OrganizationModel>> ReadAll(string token)
    {

        // Obtiene el cliente http.
        Client client = Service.GetClient("organizations/read/all");

        // Headers.
        client.AddHeader("token", token);

        var response = await client.Get<ReadAllResponse<OrganizationModel>>();

        return response;

    }



}