namespace LIN.Access.Auth.Controllers;

public class Security
{
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