namespace Actio.Common.Events
{
    public class AuthenticateUserRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }
        public string Email { get; }

        protected AuthenticateUserRejected() { }

        public AuthenticateUserRejected(string reason, string code, string email)
        {
            Reason = reason;
            Code = code;
            Email = email;
        }
    }
}
