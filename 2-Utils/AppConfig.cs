namespace Talent;

static class AppConfig
{
    public static string JwtKey { get; private set; } = "The Amazing Talent Class - עם ישראל חי! The Amazing Talent Class - עם ישראל חי!";
    public static string HostUrl { get; private set; } = null!;
    public static string ImageUrl { get; private set; } = null!;

    public static readonly string ConnectionString = null!;
    static AppConfig()
    {
        IConfigurationRoot settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        ConnectionString = settings.GetConnectionString("Restaurant")!;

        HostUrl = Environment.GetEnvironmentVariable("ASPNETCORE_URLS")!; // get environment variable about local host from launchsettings.json
        ImageUrl = HostUrl + "/api/dishes/images/";


    }
}


