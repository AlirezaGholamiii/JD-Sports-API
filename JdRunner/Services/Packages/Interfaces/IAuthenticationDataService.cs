
using JdRunner.Models.Custom.RequestResponse;
using System.Data;

namespace JdRunner.Services.Packages.Interfaces
{
	public interface IAuthenticationDataService
	{
		UserSessionResponse ValidateAndExtendSession(string apiKey, string sessionId, string controller, string action);
	}
}
