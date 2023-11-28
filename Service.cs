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
    public static HttpClient GetClient()
    {

        // Objeto.
        var client = new HttpClient()
        {
            BaseAddress = new Uri(GetURL),
            Timeout = TimeSpan.FromSeconds(20)
        };

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


}