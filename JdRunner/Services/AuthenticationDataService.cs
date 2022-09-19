
using JdRunner.Services.Packages.Interfaces;
using JdRunner.Models.Custom.RequestResponse;

namespace JdRunner.Services.Packages
{
    public class AuthenticationDataService : IAuthenticationDataService
    {

        public UserSessionResponse ValidateAndExtendSession(string apiKey, string sessionId, string controller, string action)
        {
            return new UserSessionResponse();
        }
    }
}
