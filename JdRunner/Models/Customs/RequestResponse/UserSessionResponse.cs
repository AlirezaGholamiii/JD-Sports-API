namespace JdRunner.Models.Custom.RequestResponse
{
    public class UserSessionResponse
    {
        public UserSessions UserSession { get; set; }
        public ReturnResponse ReturnResponse { get; set; }
        public UserSessionResponse()
        {
            this.ReturnResponse = new ReturnResponse();
        }
    }
}
