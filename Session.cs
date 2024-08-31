using LIN.Access.Auth.Interfaces;

namespace LIN.Access.Auth;


public sealed class SessionAuth : IISession
{


    /// <summary>
    /// Modelo de la cuenta.
    /// </summary>
    public AccountModel Account { get; private set; } = new();



    /// <summary>
    /// Token.
    /// </summary>
    public string AccountToken { get; set; }



    /// <summary>
    /// Si la sesión es activa
    /// </summary>
    public static bool IsOpen => Instance.Account.Id > 0;





    /// <summary>
    /// Iniciar sesión.
    /// </summary>
    /// <param name="user">Usuario.</param>
    /// <param name="password">Contraseña.</param>
    public static async Task<(SessionAuth? Sesion, Responses Response)> LoginWith(string user, string password)
    {

        // Cierra la sesión Actual
        CloseSession();

        // Validación de user
        var response = await Controllers.Authentication.Login(user, password);


        if (response.Response != Responses.Success)
            return (null, response.Response);


        // Datos de la instancia
        Instance.Account = response.Model;

        Instance.AccountToken = response.Token;

        return (Instance, Responses.Success);

    }



    /// <summary>
    /// Recargar sesión.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    /// <returns></returns>
    public static async Task<(SessionAuth? Sesion, Responses Response)> LoginWith(string token)
    {

        // Cierra la sesión Actual
        CloseSession();

        // Validación de user
        var response = await Controllers.Authentication.Login(token);


        if (response.Response != Responses.Success)
            return (null, response.Response);

        // Datos de la instancia
        Instance.Account = response.Model;
        Instance.AccountToken = response.Token;

        return (Instance, Responses.Success);

    }



    /// <summary>
    /// Cierra la sesión.
    /// </summary>
    public static void CloseSession()
    {
        Instance.AccountToken = string.Empty;
        Instance.Account = new();
    }






    //==================== Singleton ====================//


    private static readonly SessionAuth _instance = new();

    private SessionAuth()
    {
        AccountToken = string.Empty;
        Account = new();
    }


    public static SessionAuth Instance => _instance;


}