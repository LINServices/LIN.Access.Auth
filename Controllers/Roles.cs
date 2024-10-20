namespace LIN.Access.Auth.Controllers;

public class Roles
{

    /// <summary>
    /// Obtener los roles de un una identidad asociados a una organización.
    /// </summary>
    /// <param name="identity">Id de la identidad.</param>
    /// <param name="organization">Id de la organización.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<IdentityRolesModel>> ReadAll(int identity, int organization, string token)
    {

        // Cliente.
        Client client = Service.GetClient("identity/roles/all");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("identity", identity.ToString());
        client.AddHeader("organization", organization.ToString());

        // Respuesta
        var response = await client.Get<ReadAllResponse<IdentityRolesModel>>();

        return response;

    }
}