namespace LIN.Access.Auth.Controllers;


public class Mail
{


    /// <summary>
    /// Agrega un nuevo email a una cuenta
    /// </summary>
    /// <param name="password">Contraseña actual de la cuenta</param>
    /// <param name="modelo">Modelo del email</param>
    public async static Task<ResponseBase> Aggregate(string password, EmailModel modelo)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("password", password);

        string url = ApiServer.PathURL("security/mails/add");
        string json = JsonConvert.SerializeObject(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            HttpResponseMessage response = await client.PostAsync(url, content);

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



    /// <summary>
    /// Obtiene los emails asociados a una cuenta
    /// </summary>
    /// <param name="token">Token de la cuenta</param>
    public async static Task<ReadAllResponse<EmailModel>> ReadAll(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("mails/all");

        // Crear HttpRequestMessage y agregar el encabezado
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("token", $"{token}");

        try
        {
            // Hacer la solicitud GET
            var response = await httpClient.SendAsync(request);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadAllResponse<EmailModel>>(responseBody) ?? new();


            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    /// <summary>
    /// Reenviar el correo de verificación de mail
    /// </summary>
    /// <param name="mail">ID del mail</param>
    /// <param name="token">Token de acceso</param>
    public async static Task<ResponseBase> ResendMail(int mail, string token)
    {

        // Variables
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("mailID", mail.ToString());
        client.DefaultRequestHeaders.Add("token", token);

        string url = ApiServer.PathURL("security/mails/resend");



        try
        {
            HttpRequestMessage ms = new(HttpMethod.Post, url);

            // Envía la solicitud
            HttpResponseMessage response = await client.SendAsync(ms);

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