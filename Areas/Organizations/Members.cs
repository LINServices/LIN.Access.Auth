using LIN.Types.Cloud.Identity.Abstracts;

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