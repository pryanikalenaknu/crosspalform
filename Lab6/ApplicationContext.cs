using Lab6.Enums;

namespace Lab6;

public class ApplicationContext
{

    public static string? Domain;
    public static string? Audience;
    public static DBType DBType;
    public static string? ConnectionString;
    
    public static void InitConfiguration(IConfiguration configuration)
    {
        Audience = configuration["AuthApi:Audience"];
        Domain = configuration["AuthApi:Domain"];
        Enum.TryParse(configuration["DBType"], out DBType);
        ConnectionString = configuration["ConnectionString"];
    }
}