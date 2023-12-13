using System.Reflection;

namespace LIN.Access.Auth.Controllers;


public class Mail
{


    /// <summary>
    /// Agrega un nuevo email a una cuenta
    /// </summary>
    /// <param name="password">Contraseña actual de la cuenta</param>
    /// <param name="modelo">Modelo del email</param>
    public static async Task<ResponseBase> Aggregate(string password, EmailModel modelo)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("security/mails/add");

        // Headers.
        client.DefaultRequestHeaders.Add("password", password);

        // Resultado.
        var Content = await client.Post<ResponseBase>(modelo);

        // Retornar.
        return Content;

    }



    /// <summary>
    /// Obtiene los emails asociados a una cuenta
    /// </summary>
    /// <param name="token">Token de la cuenta</param>
    public static async Task<ReadAllResponse<EmailModel>> ReadAll(string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("mails/all");

        // Headers.
        client.DefaultRequestHeaders.Add("token", token);

        // Resultado.
        var Content = await client.Get<ReadAllResponse<EmailModel>>();

        // Retornar.
        return Content;

    }



    /// <summary>
    /// Reenviar el correo de verificación de mail
    /// </summary>
    /// <param name="mail">ID del mail</param>
    /// <param name="token">Token de acceso</param>
    public static async Task<ResponseBase> ResendMail(int mail, string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("security/mails/resend");

        // Headers.
        client.DefaultRequestHeaders.Add("token", token);
        client.DefaultRequestHeaders.Add("mailID", mail.ToString());

        // Resultado.
        var Content = await client.Post<ResponseBase>();

        // Retornar.
        return Content;

    }



}