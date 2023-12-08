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
        Client client = Service.GetClient("applications");

        client.AddParameter("id", id.ToString());

        // Headers.
        client.DefaultRequestHeaders.Add("token", token);

        // Respuesta.
        var response = await client.Get<ReadOneResponse<DirectoryModel>>();

        return response;

    }



}