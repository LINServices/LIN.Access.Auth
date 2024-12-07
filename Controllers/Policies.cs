namespace LIN.Access.Auth.Controllers;

public class Policies
{

    /// <summary>
    /// Crear una política.
    /// </summary>
    /// <param name="policy">Modelo.</param>
    /// <param name="token">Token de acceso.</param>
    /// <param name="organization">Id de la organización, en caso de quererla asignar a una.</param>
    /// <param name="assign">Si se asignara la política a la identidad.</param>
    public static async Task<CreateResponse> Create(PolicyModel policy, string token, int? organization = null, bool assign = false)
    {

        // Cliente.
        Client client = Service.GetClient("policies");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("assign", assign);
        if (organization is not null)
            client.AddHeader("organization", organization.Value);

        // Respuesta
        var response = await client.Post<CreateResponse>(policy);

        return response;

    }


    /// <summary>
    /// Obtener una política.
    /// </summary>
    /// <param name="id">Id de la política.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<PolicyModel>> Read(string id, string token)
    {

        // Cliente.
        Client client = Service.GetClient("policies");

        // Headers.
        client.AddParameter("policy", id);
        client.AddHeader("token", token);

        // Respuesta
        var response = await client.Get<ReadOneResponse<PolicyModel>>();

        return response;

    }


    /// <summary>
    /// Obtener las políticas asociadas a una organización.
    /// </summary>
    /// <param name="organization">Id de la organización.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<PolicyModel>> ReadAll(int organization, string token)
    {

        // Cliente.
        Client client = Service.GetClient("policies/organization/all");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("organization", organization);

        // Respuesta
        var response = await client.Get<ReadAllResponse<PolicyModel>>();

        return response;

    }


    /// <summary>
    /// Obtener políticas aplicables de una identidad.
    /// </summary>
    /// <param name="identity">Id de la identidad.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<PolicyModel>> Aplicable(int identity, string token)
    {

        // Cliente.
        Client client = Service.GetClient("policies/complacent/applicable");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("identity", identity);

        // Respuesta
        var response = await client.Get<ReadAllResponse<PolicyModel>>();

        return response;

    }

}