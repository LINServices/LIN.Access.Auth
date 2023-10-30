namespace LIN.Access.Auth.Controllers;


public static class Devices
{


   /// <summary>
   /// Obtiene la lista de dispositivos conectados a la cuenta.
   /// </summary>
   /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<DeviceModel>> ReadAll(string token)
    {

        // url
        var url = ApiServer.PathURL("devices");

        // Crear HttpClient
        var httpClient = new HttpClient();

        // Headers
        httpClient.DefaultRequestHeaders.Add("token", $"{token}");

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta
            var responseBody = await response.Content.ReadAsStringAsync();

            // Objecto respuesta
            var obj = JsonConvert.DeserializeObject<ReadAllResponse<DeviceModel>>(responseBody);

            return obj ?? new();


        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();
    }


}