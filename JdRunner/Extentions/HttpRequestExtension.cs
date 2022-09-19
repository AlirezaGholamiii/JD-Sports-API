
using Microsoft.AspNetCore.Http;
using JdRunner.Models.Custom.RequestResponse;
using System.Linq;




namespace JdRunner.Extentions
{
    public static class HttpRequestExtension
    {
        public static string HeaderNameApiKey { get { return "api-key"; } }
        public static string HeaderNameAuthToken { get { return "auth-token"; } }

        private static UserSessionResponse _userSessionResponse;

        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
        }
        public static void SetUserSessionResponse(this HttpRequest request, UserSessionResponse userSessionResponse)
        {
            _userSessionResponse = userSessionResponse;
        }
        public static UserSessionResponse GetUserSessionResponse(this HttpRequest request)
        {
            return _userSessionResponse;
        }
    }
}
