﻿global using LIN.Types;
global using LIN.Types.Responses;
global using LIN.Types.Auth.Models;
global using LIN.Modules;
global using LIN.Types.Auth.Enumerations;
global using Newtonsoft.Json;
global using System;
global using System.Net.Http;
global using System.Text;
global using System.Threading.Tasks;
global using LIN.Types.Enumerations;

namespace LIN.Access.Auth;


public sealed class Sesion
{

    /// <summary>
    /// Token de la sesion
    /// </summary>
    public string Token { get; set; }


    /// <summary>
    /// Genera una sesion artificial
    /// </summary>
    /// <param name="model">Modelo del usuario</param>
    /// <param name="token">Token de acceso</param>
    public static void GenerateSesion(AccountModel model, string token)
    {
        Instance.Informacion = model;
        Instance.Token = token;
        IsOpen = true;
    }


    /// <summary>
    /// Informacion del usuario
    /// </summary>
    public AccountModel Informacion { get; private set; }



    /// <summary>
    /// Si la sesion es activa
    /// </summary>
    public static bool IsOpen { get; set; } = false;


    /// <summary>
    /// Recarga o inicia una sesion
    /// </summary>
    public static async Task<(Sesion? Sesion, Responses Response)> LoginWith(string username, string password, Platforms platform, bool priv = false)
    {

        // Cierra la sesion Actual
        CloseSesion();

        // Validacion de user
        var response = await Controllers.Account.Login(username, password);

        if (response.Response != Responses.Success)
            return (null, response.Response);

        // Datos de la instancia
        Instance.Informacion = response.Model;
        Instance.Token = response.Token;

        IsOpen = true;

        return (Instance, Responses.Success);

    }



    /// <summary>
    /// Recarga o inicia una sesion
    /// </summary>
    public static async Task<(Sesion? Sesion, Responses Response)> LoginWith(string token, Platforms platform)
    {

        // Cierra la sesion Actual
        CloseSesion();

        // Validacion de user
        var response = await Controllers.Account.Login(token);

        if (response.Response != Responses.Success)
            return (null, response.Response);


        // Datos de la instancia
        Instance.Informacion = response.Model;
        Instance.Token = response.Token;

        IsOpen = true;

        return (Instance, Responses.Success);

    }





    /// <summary>
    /// Cierra la sesion
    /// </summary>
    public static void CloseSesion()
    {
        IsOpen = false;
        Instance.Informacion = new();
    }






    //==================== Singletong ====================//


    private readonly static Sesion _instance = new();

    private Sesion()
    {
        Informacion = new();
        Token = "";
    }


    public static Sesion Instance => _instance;
}
