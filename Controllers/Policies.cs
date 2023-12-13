﻿namespace LIN.Access.Auth.Controllers;


public class Policies
{


    /// <summary>
    /// Crear política en un directorio.
    /// </summary>
    /// <param name="policy">Modelo</param>
    /// <param name="token">Token</param>
    public static async Task<CreateResponse> Create(PolicyModel policy, string token)
    {

        // Cliente.
        Client client = Service.GetClient("policies");

        // Headers.
        client.AddHeader("token", token);

        // Respuesta
        var response = await client.Post<CreateResponse>(policy);

        return response;

    }



    /// <summary>
    /// Valida el acceso a un permiso (Política de permisos) de una identidad.
    /// </summary>
    /// <param name="identity">Id de la identidad.</param>
    /// <param name="policy">Id de la política</param>
    public static async Task<ReadOneResponse<bool>> Create(int identity, int policy)
    {

        // Cliente.
        Client client = Service.GetClient("policies");

        // Headers.
        client.AddParameter("identity", identity.ToString());
        client.AddParameter("policy", policy.ToString());

        // Respuesta
        var response = await client.Get<ReadOneResponse<bool>>();

        return response;

    }


}