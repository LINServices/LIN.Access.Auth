namespace LIN.Access.Auth.Controllers;


public class GroupsMembers
{


    /// <summary>
    /// Nuevo integrante.
    /// </summary>
    /// <param name="model">Modelo</param>
    /// <param name="token">Token</param>
    public static async Task<CreateResponse> Create(GroupMember model, string token)
    {

        // Cliente.
        Client client = Service.GetClient("groups/members");

        // Headers.
        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Post<CreateResponse>(model);

        return response;

    }



    /// <summary>
    /// Nuevos integrantes.
    /// </summary>
    /// <param name="identities">Identidades</param>
    /// <param name="group">Grupo</param>
    /// <param name="token">Token</param>
    public static async Task<CreateResponse> Create(List<int> identities, int group, string token)
    {

        // Cliente.
        Client client = Service.GetClient("groups/members/list");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("group", group.ToString());

        // Respuesta.
        var response = await client.Post<CreateResponse>(identities);

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
        Client client = Service.GetClient("groups/members/read/all");

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




    public static async Task<ResponseBase> Remove(string token, int group, int identity)
    {
        // Cliente.
        Client client = Service.GetClient("groups/members/remove");

        // Headers
        client.AddHeader("token", token);

        client.AddParameter("group", group.ToString());
        client.AddParameter("identity", identity.ToString());

        // Respuesta.
        var response = await client.Delete<ResponseBase>();

        return response;

    }





  
    public static async Task<ReadAllResponse<GroupModel>> OnMembers(int organization, int identity, string token)
    {

        // Cliente.
        Client client = Service.GetClient("groups/members/read/on/all");

        // Headers.
        client.AddHeader("token", token);

        client.AddParameter("organization", organization.ToString());
        client.AddParameter("identity", identity.ToString());

        // Respuesta.
        var response = await client.Get<ReadAllResponse<GroupModel>>();

        return response;

    }

}