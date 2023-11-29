namespace LIN.Access.Auth;


public static class Service
{


    /// <summary>
    /// Url base.
    /// </summary>
    private static string DefaultUrl { get; set; } = "http://linauth.somee.com/";



    /// <summary>
    /// Obtiene la Url.
    /// </summary>
    public static string Url => DefaultUrl;



    /// <summary>
    /// Obtener un cliente Http.
    /// </summary>
    public static HttpClient GetClient(string url)
    {

        // Objeto.
        var client = new HttpClient()
        {
            BaseAddress = new Uri(DefaultUrl),
            Timeout = TimeSpan.FromSeconds(20)
        };

        // Crear la url.
        Uri.TryCreate(client.BaseAddress, url, out var result);

        // Establecer la url.
        client.BaseAddress = result;

        return client;
    }



    /// <summary>
    /// Obtener un cliente Http.
    /// </summary>
    public static HttpClient GetClient(string url, Dictionary<string, string> parameters)
    {
        // Obtiene el cliente.
        var client = GetClient(url);

        // Url final.
        string finalUrl = Web.AddParameters(client.BaseAddress?.ToString() ?? "", parameters);

        // Establecer.
        client.BaseAddress = new Uri(finalUrl);

        return client;
    }



    /// <summary>
    /// Establecer la Url.
    /// </summary>
    /// <param name="url">Url default.</param>
    public static void SetDefault(string url)
    {
        DefaultUrl = url;
    }



    /// <summary>
    /// Convertir la URL.
    /// </summary>
    /// <param name="url">url</param>
    public static string PathURL(string url)
    {
        return Url + url;
    }
}