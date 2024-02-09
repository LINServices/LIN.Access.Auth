namespace LIN.Access.Auth.Controllers;


public class Directories
{









    /// <summary>
    /// Obtener un directorio.
    /// </summary>
    /// <param name="id">Id del directorio.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<GroupModel>> Read(int id, string token)
    {

        // Cliente.
        Client client = Service.GetClient("directory/read/id");

        client.AddParameter("id", id.ToString());

        // Headers.
        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Get<ReadOneResponse<GroupModel>>();

        return response;

    }



    /// <summary>
    /// Obtener los directorios.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<GroupMember>> ReadAll(string token, int organization)
    {

        // Cliente.
        Client client = Service.GetClient("directory/read/all");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("organization", organization.ToString());

        // Respuesta.
        var response = await client.Get<ReadAllResponse<GroupMember>>();

        return response;

    }



    /// <summary>
    /// Obtener los integrantes de un directorio.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<GroupMember>> ReadMembers(int directory, string token)
    {

        // Cliente.
        Client client = Service.GetClient("directory/read/members");

        // Headers.
        client.AddHeader("token", token);

        client.AddParameter("directory", directory.ToString());

        // Respuesta.
        var response = await client.Get<ReadAllResponse<GroupMember>>();

        return response;

    }


    public static async Task<CreateResponse> InsertMember(GroupMember model, string token)
    {

        // Cliente.
        Client client = Service.GetClient("directory/members");

        // Headers.
        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Post<CreateResponse>(model);

        return response;

    }



}