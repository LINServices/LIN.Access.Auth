namespace LIN.Access.Auth.Areas.Organizations;

public class Members
{

    /// <summary>
    /// Crea un integrante en una organización.
    /// </summary>
    /// <param name="modelo">Modelo del integrante</param>
    /// <param name="token">Token del administrador</param>
    /// <param name="rol">Rol del nuevo integrante</param>
    public static async Task<CreateResponse> Create(AccountModel modelo, string token, int organization)
    {

        // Cliente.
        Client client = Service.GetClient("orgs/members");

        // Headers
        client.AddHeader("token", token);
        client.AddHeader("organization", organization.ToString());

        // Respuesta.
        var response = await client.Post<CreateResponse>(modelo);

        return response;

    }


    /// <summary>
    /// Invitar a un integrante a una organización.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    /// <param name="organization">Id de la organización.</param>
    /// <param name="ids">Id de los usuarios a invitar.</param>
    public static async Task<CreateResponse> Invites(string token, int organization, List<int> ids)
    {

        // Cliente.
        Client client = Service.GetClient("orgs/members/invite");

        // Headers.
        client.AddHeader("token", token);
        client.AddParameter("organization", organization.ToString());

        // Respuesta.
        var response = await client.Post<CreateResponse>(ids);

        return response;

    }


    /// <summary>
    /// Obtiene los integrantes asociados a su organización.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    public static async Task<ReadAllResponse<SessionModel<GroupMember>>> ReadAll(string token, int organization)
    {
        // Cliente.
        Client client = Service.GetClient("orgs/members");

        // Headers
        client.AddHeader("token", token);
        client.AddHeader("organization", organization.ToString());

        // Respuesta.
        var response = await client.Get<ReadAllResponse<SessionModel<GroupMember>>>();

        return response;

    }

}