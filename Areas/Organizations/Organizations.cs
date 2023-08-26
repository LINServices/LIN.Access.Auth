namespace LIN.Access.Auth.Areas.Organizations;


public static class Organizations
{


















    public async static Task<CreateResponse> Create(OrganizationModel organization, AccountModel admin)
    {

        // Variables
        var client = new HttpClient();


        organization.AppList = new();
        organization.Members = new() { new(){
            Member = admin,
            Rol = OrgRoles.SuperManager
        } };
        admin.OrganizationAccess = null;

        string url = ApiServer.PathURL("orgs/create");
        string json = JsonConvert.SerializeObject(organization);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<CreateResponse>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }









    public async static Task<ResponseBase> UpdateWhiteListState(string token, bool estado)
    {

        // Variables
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("token", token);

        string url = ApiServer.PathURL("orgs/update/whitelist");


        url = Web.AddParameters(url, new()
        {
            {"haveWhite",$"{estado}" }
        });

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }


    public async static Task<ResponseBase> UpdateAccessState(string token, bool estado)
    {

        // Variables
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("token", token);

        string url = ApiServer.PathURL("orgs/update/access");


        url = Web.AddParameters(url, new()
        {
            {"state",$"{estado}" }
        });

        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PatchAsync(url, content);

            // Lee la respuesta del servidor
            string responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ResponseBase>(responseContent);

            return obj ?? new();

        }
        catch
        {
        }

        return new();

    }









}
