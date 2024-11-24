using Google.Apis.Auth.OAuth2.Web;
using Lab6.Model;
using Newtonsoft.Json;

namespace Lab6.Service.Impl;

public class SalesforceService(HttpClient client) : ISalesforceService
{
    public async Task<SeleforceAuthRequest> AuthAsync(SalesforceUserCred cred)
    {
        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
        
        var requestData = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"grant_type", "password" },
            {"client_id", ApplicationContext.SalesforceClientId },
            {"client_secret", ApplicationContext.SalesforceClientSecret },
            {"username", cred.User },
            {"password", $"{cred.Pass}{cred.Token}" } 
        });

        var response = await client.PostAsync(ApplicationContext.SalesforceLoginDomain, requestData);
        if (!response.IsSuccessStatusCode) throw new Exception("Cannot auth with saleforce");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
           
        return new SeleforceAuthRequest()
        {
            AuthToken = values["access_token"],
            InstanceUrl = values["instance_url"]
        };
    }
}