using Lab6.Model;

namespace Lab6.Service;

public interface ISalesforceService
{
    Task<SeleforceAuthRequest> AuthAsync(SalesforceUserCred cred);
}