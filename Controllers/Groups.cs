namespace LIN.Access.Auth.Controllers;

public class Groups
{

    /// <summary>
    /// Crear un grupo en una organización.
    /// </summary>
    /// <param name="group">Modelo</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<CreateResponse> Create(GroupModel group, string token)
    {

        // Cliente.
        Client client = Service.GetClient("groups");

        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Post<CreateResponse>(group);

        return response;

    }


    /// <summary>
    /// Obtener un grupo.
    /// </summary>
    /// <param name="id">Id del grupo.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<GroupModel>> Read(int id, string token)
    {

        // Cliente.
        Client client = Service.GetClient($"groups/{id}");

        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Get<ReadOneResponse<GroupModel>>();

        return response;

    }


    /// <summary>
    /// Obtener un grupo.
    /// </summary>
    /// <param name="id">Id de la identidad del grupo.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<GroupModel>> ReadByIdentity(int id, string token)
    {

        // Cliente.
        Client client = Service.GetClient($"groups/identity/{id}");

        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Get<ReadOneResponse<GroupModel>>();

        return response;

    }


    /// <summary>
    /// Obtener los grupos asociados a una organización.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<GroupModel>> ReadAll(string token, int organization)
    {

        // Cliente.
        Client client = Service.GetClient("groups");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter("organization", organization);

        // Respuesta.
        var response = await client.Get<ReadAllResponse<GroupModel>>();

        return response;

    }

}