namespace LIN.Access.Auth.Areas.Organizations;


public class Applications
{


    /// <summary>
    /// Crea el acceso permitido a una aplicación en una organización.
    /// </summary>
    /// <param name="uId">Uid de la aplicación</param>
    /// <param name="token">Token del administrador</param>
    public static async Task<CreateResponse> Create(string uId, string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("orgs/applications/insert");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter(new()
        {
           {"appUid", $"{uId}"}
        });

        // Get.
        var (Content, _) = await client.Get<CreateResponse>();

        return Content;

    }



    /// <summary>
    /// Obtiene la lista de aplicaciones permitidas en tu organización.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    public static async Task<ReadAllResponse<ApplicationModel>> ReadAll(string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("orgs/applications");

        // Headers.
        client.AddHeader("token", token);

      
        // Get.
        var (Content, _) = await client.Get<ReadAllResponse<ApplicationModel>>();

        return Content;

    }



    /// <summary>
    /// Obtiene la lista de aplicaciones según un parámetro que no coincidan con la de una organización.
    /// </summary>
    /// <param name="param">Parámetro de búsqueda</param>
    /// <param name="token">Token de acceso</param>
    public static async Task<ReadAllResponse<ApplicationModel>> Search(string param, string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("orgs/applications/search");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter(new()
        {
           {"param", $"{param}"}
        });

        // Get.
        var (Content, _) = await client.Get<ReadAllResponse<ApplicationModel>>();

        return Content;

    }



}