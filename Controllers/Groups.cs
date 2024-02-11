namespace LIN.Access.Auth.Controllers;


public class Groups
{


    /// <summary>
    /// Crear grupo.
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
    /// <param name="id">Id del directorio.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<GroupModel>> Read(int id, string token)
    {

        // Cliente.
        Client client = Service.GetClient("groups");

        client.AddHeader("id", id.ToString());
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
        Client client = Service.GetClient("groups/all");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("organization", organization.ToString());

        // Respuesta.
        var response = await client.Get<ReadAllResponse<GroupModel>>();

        return response;

    }



    /// <summary>
    /// Obtener los integrantes de un grupo.
    /// </summary>
    /// <param name="group">Id del grupo</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<GroupMember>> ReadMembers(int group, string token)
    {

        // Cliente.
        Client client = Service.GetClient("groups/read/members");

        // Headers.
        client.AddHeader("token", token);

        client.AddParameter("group", group.ToString());

        // Respuesta.
        var response = await client.Get<ReadAllResponse<GroupMember>>();

        return response;

    }



    /// <summary>
    /// Buscar en los integrantes de un grupo.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    /// <param name="group">Id del grupo.</param>
    /// <param name="pattern">Patron de búsqueda.</param>
    public static async Task<ReadAllResponse<IdentityModel>> Search(string token, int group, string pattern)
    {
        // Cliente.
        Client client = Service.GetClient("groups/members/search");

        // Headers
        client.AddHeader("token", token);
        client.AddHeader("group", group.ToString());

        client.AddParameter("pattern", pattern);

        // Respuesta.
        var response = await client.Get<ReadAllResponse<IdentityModel>>();

        return response;

    }



}