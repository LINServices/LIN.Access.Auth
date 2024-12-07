namespace LIN.Access.Auth.Controllers;

public class Security
{

    /// <summary>
    /// Agregar correo.
    /// </summary>
    /// <param name="mail">Correo.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<CreateResponse> AddMail(string mail, string token)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("security/mail");

        client.AddHeader("token", token);
        client.AddParameter("mail", mail);

        // Resultado.
        var Content = await client.Post<CreateResponse>();

        // Retornar.
        return Content;

    }


    /// <summary>
    /// Validar correo.
    /// </summary>
    /// <param name="mail">Correo.</param>
    /// <param name="otp">Código OTP.</param>
    public static async Task<CreateResponse> ValidateMail(string mail, string otp)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("security/validate");

        client.AddParameter("code", otp);
        client.AddParameter("mail", mail);

        // Resultado.
        var Content = await client.Post<CreateResponse>();

        // Retornar.
        return Content;

    }


    /// <summary>
    /// Olvidar la contraseña.
    /// </summary>
    /// <param name="user">Usuario.</param>
    public static async Task<CreateResponse> ForgetPassword(string user)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("security/forget/password");

        client.AddParameter("user", user);

        // Resultado.
        var Content = await client.Post<CreateResponse>();

        // Retornar.
        return Content;

    }


    /// <summary>
    /// Resetear contraseña.
    /// </summary>
    /// <param name="user">Usuario.</param>
    /// <param name="code">Código OTP.</param>
    /// <param name="newPassword">Nueva contraseña.</param>
    public static async Task<CreateResponse> ResetPassword(string user, string code, string newPassword)
    {

        // Cliente HTTP.
        Client client = Service.GetClient("security/reset");

        client.AddParameter("unique", user);
        client.AddParameter("code", code);
        client.AddParameter("newPassword", newPassword);

        // Resultado.
        var Content = await client.Post<CreateResponse>();

        // Retornar.
        return Content;

    }

}