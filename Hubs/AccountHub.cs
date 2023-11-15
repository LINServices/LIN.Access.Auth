﻿namespace LIN.Access.Auth.Hubs;


public sealed class AccountHub
{


    //======== Eventos ========//


    /// <summary>
    /// Evento: Cuando se recibe un comando.
    /// </summary>
    public event EventHandler<string>? OnReceivingCommand;



    /// <summary>
    /// Evento: Cuando un dispositivo se va.
    /// </summary>
    public event EventHandler<string>? OnDeviceLeaves;



    /// <summary>
    /// Evento: Cuando un dispositivo se une.
    /// </summary>
    public event EventHandler<DeviceModel>? OnDeviceJoins;



    /// <summary>
    /// Evento cuando la lista de dispositivos cambio.
    /// </summary>
    public event EventHandler<string>? OnDeviceChange;



    /// <summary>
    /// Evento cuando se reciben la lista de dispositivos.
    /// </summary>
    public event EventHandler<List<DeviceModel>>? OnReceiveDevicesList;




    //======== Propiedades ========//


    /// <summary>
    /// Conexión del Hub.
    /// </summary>
    private HubConnection? HubConnection { get; set; }



    /// <summary>
    /// Obtiene el ID de usuario asignado este dispositivo.
    /// </summary>
    public string ID => HubConnection?.ConnectionId ?? string.Empty;



    /// <summary>
    /// Modelo del dispositivo.
    /// </summary>
    public DeviceModel? DeviceModel { get; set; }



    /// <summary>
    /// Constructor.
    /// </summary>
    public AccountHub(Task<DeviceModel> task)
    {
        A(task);
    }


    public AccountHub(DeviceModel model)
    {
        DeviceModel = model;
        _ = Suscribe();
    }



    private async void A(Task<DeviceModel> task)
    {
        DeviceModel = await task;
        await Suscribe();
    }




    /// <summary>
    /// Reconecta la conexión
    /// </summary>
    public async void Reconnect()
    {
        await Suscribe();

    }



    public async void ReconnectAndUpdate()
    {
        if (HubConnection?.State != HubConnectionState.Connected)
        {
            await Suscribe();
            GetDevicesList(DeviceModel?.Cuenta ?? 0);
        }


    }



    /// <summary>
    /// Conecta el Hub
    /// </summary>
    private async Task Suscribe()
    {
        try
        {
            // Crea la conexión al HUB
            HubConnection = new HubConnectionBuilder()
               .WithUrl(ApiServer.PathURL("realTime/service"))
               .WithAutomaticReconnect()
               .Build();


            // Evento cuando se reciba una tarea
            HubConnection.On<string>("devicecommand", SendTaskEvent);

            // Evento cuando se reciba una tarea
            HubConnection.On<string>("accountcommand", SendTaskEvent);


            // Evento alguien salio del
            HubConnection.On<string>("leaveevent", (id) => { OnDeviceLeaves?.Invoke(null, id); });


            // Evento cuando se reciba una tarea
            HubConnection.On<DeviceModel>("newdevice", SendNewDeviceEvent);


            // Evento cuando se obtienen los dispositivos
            HubConnection.On<List<DeviceModel>, string>("devicesList", SendAllEvent);


            // Evento cuando se esta testeando la conexión
            HubConnection.On("ontest", async () =>
            {
                try
                {
                    if (DeviceModel == null)
                        return;
                    else
                        await HubConnection!.InvokeAsync("ReceiveTestStatus", DeviceModel.Cuenta, DeviceModel.BateryLevel, DeviceModel.BateryConected);
                }
                catch
                {
                }
            });

            // Inicia la conexión
            await HubConnection.StartAsync();

            // Suscribe al grupo
            await HubConnection.InvokeAsync("Join", DeviceModel);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error HUB Account: " + ex);
        }

    }



    /// <summary>
    /// Obtiene la lista de dispositivos
    /// </summary>
    public async void GetDevicesList(int cuenta)
    {

        // Comprueba la conexión
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecución
        try
        {
            await HubConnection!.InvokeAsync("GetDevicesList", cuenta);
        }
        catch
        {
        }

    }



    /// <summary>
    /// Envía para hacer un Test
    /// </summary>
    public async void TestConnection()
    {

        // No existe
        if (HubConnection == null || DeviceModel == null)
            return;

        // Test de conexion
        if (HubConnection.State != HubConnectionState.Connecting && HubConnection.State != HubConnectionState.Connected)
        {
            // Intenta parar el hub actual
            try
            {
                await HubConnection.StopAsync();
            }
            catch { }

            // Inicia la nueva ejecucion
            try
            {
                await HubConnection.StartAsync();
                await HubConnection.InvokeAsync("Join", DeviceModel);
            }
            catch { }

        }


        // Ejecucion
        if (HubConnection.State == HubConnectionState.Connected)
        {
            try
            {
                await HubConnection.InvokeAsync("TestDevices", DeviceModel.Cuenta);
            }
            catch
            {
            }
        }


    }



    /// <summary>
    /// Envía la informacion de un nuevo contacto
    /// </summary>
    public async void SendContactModel(int cuenta, int contactoID)
    {
        try
        {
            await HubConnection!.InvokeAsync("AddContact", $"{cuenta}", contactoID);
        }
        catch
        {

        }

    }



    /// <summary>
    /// Cierra la conexion
    /// </summary>
    public async Task CloseSesion()
    {

        // Comprueba la conexion
        if (HubConnection?.State != HubConnectionState.Connected || DeviceModel == null)
            return;

        // Ejecucion
        try
        {
            await HubConnection!.InvokeAsync("Leave", DeviceModel.Cuenta);
        }
        catch
        {
        }

    }



    /// <summary>
    /// Envía la nueva informacion de la bateria
    /// </summary>
    public async void SendBattery()
    {

        // Comprueba la conexion
        if (HubConnection?.State != HubConnectionState.Connected || DeviceModel == null)
            return;

        // Ejecucion
        try
        {
            await HubConnection!.InvokeAsync("ReceiveTestStatus", DeviceModel.Cuenta, DeviceModel.BateryLevel, DeviceModel.BateryConected);
        }
        catch
        {
        }
    }



    /// <summary>
    /// Envía un comando
    /// </summary>
    public async void SendCommand(string to, string comando)
    {

        // Comprueba la conexion
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecucion
        try
        {
            await HubConnection!.InvokeAsync("SendDeviceCommand", to, comando);
        }
        catch
        {
        }

    }


    /// <summary>
    /// Envía un comando
    /// </summary>
    public async void SendCommand(int to, string comando)
    {

        Console.WriteLine("Sending conection");
        // Comprueba la conexion
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecucion
        try
        {
            Console.WriteLine("Doing");
            await HubConnection!.InvokeAsync("SendAccountCommand", to, comando);
        }
        catch
        {
        }
    }




    //==== Disparadores ====//


    /// <summary>
    /// Envía el evento de tarea
    /// </summary>
    private void SendTaskEvent(string contenido)
    {
        OnReceivingCommand?.Invoke(null, contenido);
    }



    /// <summary>
    /// Envía el evento de tarea
    /// </summary>
    private void SendNewDeviceEvent(DeviceModel modelo)
    {
        OnDeviceJoins?.Invoke(null, modelo);
    }



    /// <summary>
    /// Envía el evento
    /// </summary>
    private void SendAllEvent(List<DeviceModel> contenido, string myID)
    {
        OnReceiveDevicesList?.Invoke(null, contenido);
    }



    public async void SendTest()
    {
        // Comprueba la conexion
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        try
        {
            await HubConnection!.InvokeAsync("ReceiveTestStatus", DeviceModel?.Cuenta, DeviceModel?.BateryLevel, DeviceModel?.BateryConected);
        }
        catch
        {
        }
    }


    /// <summary>
    /// Envía el evento
    /// </summary>
    public async void SendNotificacion(List<int> contenido)
    {
        // Comprueba la conexion
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecucion
        try
        {
            await HubConnection!.InvokeAsync("AddInvitation", contenido);
        }
        catch
        {
        }
    }




}