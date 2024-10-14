namespace LIN.Access.Auth.Controllers;


public class Policies
{


    /// <summary>
    /// Crear política en un directorio.
    /// </summary>
    /// <param name="policy">Modelo</param>
    /// <param name="token">Token</param>
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



    ///// <summary>
    ///// Valida el acceso a un permiso (Política de permisos) de una identidad.
    ///// </summary>
    ///// <param name="identity">Id de la identidad.</param>
    ///// <param name="policy">Id de la política</param>
    //public static async Task<ReadOneResponse<bool>> Validate(int identity, int policy)
    //{

    //    // Cliente.
    //    Client client = Service.GetClient("policies/access");

    //    // Headers.
    //    client.AddParameter("identity", identity.ToString());
    //    client.AddParameter("policy", policy.ToString());

    //    // Respuesta
    //    var response = await client.Get<ReadOneResponse<bool>>();

    //    return response;

    //}




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