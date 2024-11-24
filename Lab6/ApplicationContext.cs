using Lab6.Enums;
using Lab6.Model;

namespace Lab6;

public class ApplicationContext
{

    public static string? Domain;
    public static string? Audience;
    public static DBType DBType;
    public static string? ConnectionString;

    public static string? SalesforceClientId;
    public static string? SalesforceClientSecret;
    public static string? SalesforceLoginDomain;

    public static SalesforceUserCred DefaultUserCred;
    public static string? SalesforceUserName;
    public static string? SalesforceUserPass;
    public static string? SalesforceUserToken;
    
    public static void InitConfiguration(IConfiguration configuration)
    {
        Audience = configuration["AuthApi:Audience"];
        Domain = configuration["AuthApi:Domain"];
        Enum.TryParse(configuration["DBType"], out DBType);
        ConnectionString = configuration["ConnectionString"];        
        SalesforceClientId = configuration["Salesforce:ClientId"];
        SalesforceClientSecret = configuration["Salesforce:ClientSecret"];
        SalesforceLoginDomain = configuration["Salesforce:Domain"];      
        
        SalesforceUserName = configuration["Salesforce:User:UserName"];
        SalesforceUserPass = configuration["Salesforce:User:Pass"];
        SalesforceUserToken = configuration["Salesforce:User:Token"];

        DefaultUserCred = new SalesforceUserCred
        {
            User = SalesforceUserName,
            Pass = SalesforceUserPass,
            Token = SalesforceUserToken
        };

    }
}