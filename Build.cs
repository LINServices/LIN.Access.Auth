namespace LIN.Access.Auth;


public class Build
{

    internal static string Application { get; set; } = string.Empty;

    public static void SetAuth(string app)
    {
        Application = app;
    }


    public static void Init()
    {
        LIN.Access.Service.SetDefault("http://api.identity.linapps.co/");
    }

}