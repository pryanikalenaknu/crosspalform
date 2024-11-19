namespace Lab5;

public class ApplicationContext
{

    public static string? Domain;
    public static string? Id;
    public static string? Secret;
    public static string? Audience;
    
    public static void InitConfiguration(IConfiguration configuration)
    {
        Id = configuration["AuthApi:Id"];
        Secret = configuration["AuthApi:Secret"];
        Audience = configuration["AuthApi:Audience"];
        Domain = configuration["AuthApi:Domain"];
    }
}