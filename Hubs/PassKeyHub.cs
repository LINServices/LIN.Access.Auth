namespace LIN.Access.Auth.Hubs;


public sealed class PassKeyHub
{

    //======== Eventos ========//


    /// <summary>
    /// Recive un intento (Admin)
    /// </summary>
    public event EventHandler<PassKeyModel>? OnRecieveIntentAdmin;



    /// <summary>
    /// Recibe un intento (Client)
    /// </summary>
    public event EventHandler<PassKeyModel>? OnRecieveResponse;



    //======== Propiedades ========//


    /// <summary>
    /// Conexion del Hub
    /// </summary>
    private HubConnection? HubConnection { get; set; }



    /// <summary>
    /// Obtiene el ID de usuario asignado este dispositivo
    /// </summary>
    public string ID
    {
        get
        {
            return HubConnection?.ConnectionId ?? string.Empty;
        }
    }



    /// <summary>
    /// Usuario 
    /// </summary>
    public string Account { get; set; }


    /// <summary>
    /// Si es una sesion de Admin
    /// </summary>
    public bool IsAdmin { get; set; }


    private string AppKey = "";


    /// <summary>
    /// Constructor de un HUB
    /// </summary>
    public PassKeyHub(string account, string appKey,  bool isAdmin = false)
    {
        this.Account = account;
        this.IsAdmin = isAdmin;
        this.AppKey = appKey;
    }


    /// <summary>
    /// Reconecta la conexion
    /// </summary>
    public async void Reconnect()
    {
        await Suscribe();
    }



    /// <summary>
    /// Cierra la conexion
    /// </summary>
    public async void Disconet()
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
            // Crea la conexion al HUB
            HubConnection = new HubConnectionBuilder()
                 .WithUrl(ApiServer.PathURL("realtime/auth/passkey"))
                 .WithAutomaticReconnect()
                 .Build();


            // Recibe un intento Admin
            HubConnection.On<PassKeyModel>("newintent", (pass) =>
            {
                OnRecieveIntentAdmin?.Invoke(null, pass);
            });


            // Recibe una respuesta (Cliente)
            HubConnection.On<PassKeyModel>("#response", (pass) =>
            {
                OnRecieveResponse?.Invoke(null, pass);
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
