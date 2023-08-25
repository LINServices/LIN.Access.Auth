namespace LIN.Access.Auth.Controllers;


public static class Organizations
{


	/// <summary>
	/// Obtiene los datos de una cuenta especifica
	/// </summary>
	/// <param name="id">ID de la cuenta</param>
	public async static Task<ReadAllResponse<AccountModel>> ReadMembers(string token)
	{

		// Crear HttpClient
		using var httpClient = new HttpClient();


		httpClient.DefaultRequestHeaders.Add("token", $"{token}");
		// ApiServer de la solicitud GET
		string url = ApiServer.PathURL("orgs/members");


		try
		{

			// Hacer la solicitud GET
			HttpResponseMessage response = await httpClient.GetAsync(url);

			// Leer la respuesta como una cadena
			string responseBody = await response.Content.ReadAsStringAsync();


			var obj = JsonConvert.DeserializeObject<ReadAllResponse<AccountModel>>(responseBody);

			return obj ?? new();


		}
		catch (Exception e)
		{
			Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
		}


		return new();
	}



	/// <summary>
	/// Obtiene los datos de una cuenta especifica
	/// </summary>
	/// <param name="id">ID de la cuenta</param>
	public async static Task<ReadAllResponse<ApplicationModel>> ReadApps(string token)
	{

		// Crear HttpClient
		using var httpClient = new HttpClient();


		httpClient.DefaultRequestHeaders.Add("token", $"{token}");
		// ApiServer de la solicitud GET
		string url = ApiServer.PathURL("orgs/apps");


		try
		{

			// Hacer la solicitud GET
			HttpResponseMessage response = await httpClient.GetAsync(url);

			// Leer la respuesta como una cadena
			string responseBody = await response.Content.ReadAsStringAsync();


			var obj = JsonConvert.DeserializeObject<ReadAllResponse<ApplicationModel>>(responseBody);

			return obj ?? new();


		}
		catch (Exception e)
		{
			Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
		}


		return new();
	}





	public async static Task<CreateResponse> Create(AccountModel modelo, string token, OrgRoles rol)
	{

		// Variables
		var client = new HttpClient();

		client.DefaultRequestHeaders.Add("token", token);
		client.DefaultRequestHeaders.Add("rol", $"{(int)rol}");

		string url = ApiServer.PathURL("orgs/create/member");
		string json = JsonConvert.SerializeObject(modelo);

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





	public async static Task<ReadAllResponse<ApplicationModel>> SearchApps(string app)
	{

		// Crear HttpClient
		using var httpClient = new HttpClient();


		// ApiServer de la solicitud GET
		string url = ApiServer.PathURL("orgs/search/apps");


		url = Web.AddParameters(url, new()
		{
			{"param", app }
		});

		try
		{

			// Hacer la solicitud GET
			HttpResponseMessage response = await httpClient.GetAsync(url);

			// Leer la respuesta como una cadena
			string responseBody = await response.Content.ReadAsStringAsync();


			var obj = JsonConvert.DeserializeObject<ReadAllResponse<ApplicationModel>>(responseBody);

			return obj ?? new();


		}
		catch (Exception e)
		{
			Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
		}


		return new();
	}





    public async static Task<CreateResponse> Create(string uId, string token)
    {

        // Variables
        var client = new HttpClient();
		client.DefaultRequestHeaders.Add("token", token);

        string url = ApiServer.PathURL("orgs/insert/app");

        url = Web.AddParameters(url, new()
        {
            {"appUid", uId }
        });



        try
        {
            // Contenido
            StringContent content = new("", Encoding.UTF8, "application/json");

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

}
