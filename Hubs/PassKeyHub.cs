namespace LIN.Access.Auth.Hubs;


public sealed class PassKeyHub
{


    //======== Eventos ========//


    /// <summary>
    /// Recibe un intento (Admin).
    /// </summary>
    public event EventHandler<PassKeyModel>? OnReceiveIntent;



    /// <summary>
    /// Recibe un intento (Client).
    /// </summary>
    public event EventHandler<PassKeyModel>? OnReceiveResponse;



    //======== Propiedades ========//


    /// <summary>
    /// Conexión del Hub
    /// </summary>
    private HubConnection? HubConnection { get; set; }



    /// <summary>
    /// Obtiene el ID de usuario asignado este dispositivo
    /// </summary>
    public string ID => HubConnection?.ConnectionId ?? string.Empty;


    /// <summary>
    /// Usuario 
    /// </summary>
    public string Account { get; set; }


    /// <summary>
    /// Si es una sesión de Admin
    /// </summary>
    public bool IsAdmin { get; set; }


    /// <summary>
    /// Llave de la aplicación.
    /// </summary>
    private string AppKey { get; set; } = string.Empty;


    /// <summary>
    /// Constructor de un HUB
    /// </summary>
    public PassKeyHub(string account, string appKey, bool isAdmin = false)
    {
        Account = account;
        IsAdmin = isAdmin;
        AppKey = appKey;
    }


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
            if (HubConnection != null)
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
               .WithUrl(ApiServer.PathURL("realtime/auth/passkey"))
               .WithAutomaticReconnect()
               .WithKeepAliveInterval(TimeSpan.FromSeconds(2))
               .WithStatefulReconnect()
               .Build();


            // Recibe un intento Admin
            HubConnection.On<PassKeyModel>("newintent", (pass) =>
            {
                OnReceiveIntent?.Invoke(null, pass);
            });


            // Recibe una respuesta (Cliente)
            HubConnection.On<PassKeyModel>("#response", (pass) =>
            {
                OnReceiveResponse?.Invoke(null, pass);
            });


            // Inicia la conexión
            await HubConnection.StartAsync();


            if (IsAdmin)
            {
                // Suscribe al grupo
                await HubConnection.InvokeAsync("JoinAdmin", Account);
            }



        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error HUB Account: " + ex);
        }

    }





    public async void SendIntent(PassKeyModel intent)
    {
        try
        {
            intent.Application ??= new();
            intent.Application.Key = AppKey;
            await HubConnection!.InvokeAsync("JoinIntent", intent);
        }
        catch
        {
        }
    }


    public async void SendStatus(PassKeyModel intent)
    {
        try
        {
            await HubConnection!.InvokeAsync("ReceiveRequest", intent);
        }
        catch
        {

        }
    }


    //==== Disparadores ====//





}