namespace LIN.Access.Auth.Controllers;


public class Directories
{


    /// <summary>
    /// Obtener un directorio.
    /// </summary>
    /// <param name="id">Id del directorio.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<DirectoryModel>> Read(int id, string token)
    {

        // Cliente.
        Client client = Service.GetClient("directory/read/id");

        client.AddParameter("id", id.ToString());

        // Headers.
        client.DefaultRequestHeaders.Add("token", token);

        // Respuesta.
        var response = await client.Get<ReadOneResponse<DirectoryModel>>();

        return response;

    }



    /// <summary>
    /// Obtener los directorios.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<DirectoryMember>> ReadAll(string token)
    {

        // Cliente.
        Client client = Service.GetClient("directory/read/all");

        // Headers.
        client.AddHeader("token", token);

        // Respuesta.
        var response = await client.Get<ReadAllResponse<DirectoryMember>>();

        return response;

    }



}