namespace LIN.Access.Auth.Interfaces;


public interface IISession
{


    /// <summary>
    /// Modelo de la cuenta.
    /// </summary>
    public AccountModel Account { get; }



    /// <summary>
    /// Token.
    /// </summary>
    public string AccountToken { get; set; }



    /// <summary>
    /// Si la sesión es activa
    /// </summary>
    public static bool IsOpen { get; }


}