namespace LIN.Access.Auth.Controllers;


public class Groups
{




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
    /// Obtener los directorios.
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




}