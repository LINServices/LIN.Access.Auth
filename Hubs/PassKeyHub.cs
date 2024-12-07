namespace LIN.Access.Auth.Hubs;

/// <summary>
/// Constructor de un HUB
/// </summary>
public sealed class PassKeyHub(string account, string appKey, string token, bool isAdmin = false)
{

    /// <summary>
    /// Recibe un intento (Admin).
    /// </summary>
    public event EventHandler<PassKeyModel>? OnReceiveIntent;


    /// <summary>
    /// Recibe un intento (Client).
    /// </summary>
    public event EventHandler<PassKeyModel>? OnReceiveResponse;


    /// <summary>
    /// Conexión del Hub
    /// </summary>
    private HubConnection? HubConnection { get; set; }


    /// <summary>
    /// Obtiene el Id de usuario asignado este dispositivo
    /// </summary>
    public string Id => HubConnection?.ConnectionId ?? string.Empty;


    /// <summary>
    /// Usuario 
    /// </summary>
    public string Account { get; set; } = account;


    /// <summary>
    /// Si es una sesión de Admin
    /// </summary>
    public bool IsAdmin { get; set; } = isAdmin;


    /// <summary>
    /// Llave de la aplicación.
    /// </summary>
    private string AppKey { get; set; } = appKey;


    /// <summary>
    /// Llave de la aplicación.
    /// </summary>
    private string Token { get; set; } = token;


    /// <summary>
    /// Reconecta la conexión.
    /// </summary>
    public async void Reconnect()
    {
        await Suscribe();
    }


    /// <summary>
    /// Cierra la conexión.
    /// </summary>
    public async void Disconnect()
    {
        try
        {
            if (HubConnection is not null)
                await HubConnection.StopAsync();
        }
        catch
        {
        }

    }


    /// <summary>
    /// Conecta el Hub
    /// </summary>
    public async Task Suscribe()
    {
        try
        {

            // Crea la conexión al HUB
            HubConnection = new HubConnectionBuilder()
                .WithUrl(Service._Service.PathURL("realTime/auth/passkey"))
                .WithAutomaticReconnect()
                .Build();

            // Recibe un intento Admin
            HubConnection.On<PassKeyModel>("#attempts", (pass) =>
            {
                OnReceiveIntent?.Invoke(null, pass);
            });

            // Recibe una respuesta (Cliente)
            HubConnection.On<PassKeyModel>("#responses", (pass) =>
            {
                OnReceiveResponse?.Invoke(null, pass);
            });

            // Inicia la conexión
            await HubConnection.StartAsync();

            if (IsAdmin)
            {
                // Suscribe al grupo
                await HubConnection.InvokeAsync("JoinAdmin", Token);
            }

        }
        catch (Exception)
        {
        }

    }


    /// <summary>
    /// Enviar intento.
    /// </summary>
    /// <param name="intent">Modelo.</param>
    public async void SendIntent(PassKeyModel intent)
    {
        try
        {
            await HubConnection!.InvokeAsync("JoinIntent", intent);
        }
        catch
        {
        }
    }


    /// <summary>
    /// Enviar nuevo estado.
    /// </summary>
    /// <param name="intent">Id del estado.</param>
    public async void SendStatus(PassKeyModel intent)
    {
        try
        {
            await HubConnection!.InvokeAsync("ReceiveRequest", intent);
        }
        catch (Exception)
        {
        }
    }

}