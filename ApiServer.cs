namespace LIN.Access.Auth;


public static class ApiServer
{

    private static bool IsSeted = false;
    private static string SetedUrl = "";

    public static string GetURL
    {
        get
        {

            if (IsSeted)
                return SetedUrl;

            return "http://linauth.somee.com/";
        }
    }



    public static void SetUrl(string url)
    {
        IsSeted = true;
        SetedUrl = url;
    }

    public static string PathURL(string value)
    {
        return GetURL + value;
    }


}
