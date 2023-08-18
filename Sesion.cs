namespace LIN.Access.Auth;


public sealed class Session
{



    /// <summary>
    /// Información del usuario
    /// </summary>
    public LIN.Types.Auth.Models.AccountModel Account { get; private set; } = new();


    public string AccountToken { get; set; }



    /// <summary>
    /// Si la sesión es activa
    /// </summary>
    public static bool IsOpen { get => Instance.Account.ID > 0; }





    /// <summary>
    /// Recarga o inicia una sesión
    /// </summary>
    public static async Task<(Session? Sesion, Responses Response)> LoginWith(string user, string password)
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
    /// Recarga o inicia una sesión
    /// </summary>
    public static async Task<(Session? Sesion, Responses Response)> LoginWith(string token)
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
    /// Cierra la sesión
    /// </summary>
    public static void CloseSession()
    {
        Instance.Account = new();
    }






    //==================== Singleton ====================//


    private readonly static Session _instance = new();

    private Session()
    {
        Account = new();
    }


    public static Session Instance => _instance;
}
