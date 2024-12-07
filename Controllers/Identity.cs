namespace LIN.Access.Auth.Controllers;

public class Identity
{

    /// <summary>
    /// Agregar un rol a una identidad.
    /// </summary>
    /// <param name="model">Modelo.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ResponseBase> Create(IdentityRolesModel model, string token)
    {

        // Cliente.
        Client client = Service.GetClient("identity");

        // Headers.
        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Post<ResponseBase>(model);

        return response;

    }


    /// <summary>
    /// Eliminar un rol de una identidad.
    /// </summary>
    /// <param name="organization">Id de la organización.</param>
    /// <param name="identity">Id de la identidad.</param>
    /// <param name="rol">Rol a eliminar.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ResponseBase> Remove(int organization, int identity, LIN.Types.Cloud.Identity.Enumerations.Roles rol, string token)
    {

        // Cliente.
        Client client = Service.GetClient("identity/roles");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("organization", organization.ToString());
        client.AddHeader("rol", ((int)rol).ToString());
        client.AddHeader("identity", identity.ToString());

        // Respuesta.
        var response = await client.Delete<ResponseBase>();

        return response;

    }

}