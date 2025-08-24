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

}