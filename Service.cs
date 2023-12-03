﻿namespace LIN.Access.Auth;


public static class Service
{


    /// <summary>
    /// Url base.
    /// </summary>
    private static string DefaultUrl { get; set; } = "http://api.identity.linapps.co/";



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
            Timeout = TimeSpan.FromSeconds(20)
        };

        // Default.
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.Add(new("LIN App"));

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
        Uri.TryCreate(new Uri(Url), url, out Uri? result);
        return result?.ToString() ?? "";
    }



    /// <summary>
    /// Convertir la URL.
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="values">Valores de consulta.</param>
    public static string PathURL(string url, Dictionary<string, string> values)
    {
        // Unir la URL.
        Uri.TryCreate(new Uri(Url), url, out Uri? result);

        // Resultado.
        string uri = result?.ToString() ?? "";

        // Agregar parametros.
        uri = Web.AddParameters(uri, values);

        return uri;
    }



}