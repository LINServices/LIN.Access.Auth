namespace LIN.Access.Auth.Controllers;


public class Directories
{


    /// <summary>
    /// Obtener los directorios base asociados.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    /// <param name="organization">Id de la organización.</param>
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



    /// <summary>
    /// Nuevo integrante.
    /// </summary>
    /// <param name="model">Modelo</param>
    /// <param name="token">Token</param>
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