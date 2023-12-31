﻿namespace LIN.Access.Auth.Areas.Organizations;


public static class Organizations
{


    /// <summary>
    /// Crea una nueva organización.
    /// </summary>
    /// <param name="organization">Modelo de la organización</param>
    /// <param name="admin">Usuario administrador</param>
    public static async Task<CreateResponse> Create(OrganizationModel organization, AccountModel admin)
    {

        // Obtiene el cliente http.
        Client client = Service.GetClient("organizations/create");

        // Organizar el modelo.
        admin.OrganizationAccess = null;
        organization.Members =
        [
            new()
            {
                Member = admin,
                Rol = OrgRoles.SuperManager
            }
        ];

        var response = await client.Post<CreateResponse>(organization);

        return response;

    }



    /// <summary>
    /// Obtiene una organización.
    /// </summary>
    /// <param name="id">ID de la organización</param>
    /// <param name="token">token de acceso.</param>
    public static async Task<ReadOneResponse<OrganizationModel>> Read(int id, string token)
    {

        // Obtiene el cliente http.
        Client client = Service.GetClient("organizations/read/id");

        // Headers.
        client.AddHeader("token", token);

        // Consultas.
        client.AddParameter("id", id.ToString());   

        var response = await client.Get<ReadOneResponse<OrganizationModel>>();

        return response;

    }


}