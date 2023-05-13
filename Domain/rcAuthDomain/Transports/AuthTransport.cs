namespace rcAuthDomain.Transports
{
    public class AuthTransport
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public AuthTransport() { }

        public AuthTransport(AuthTransport transport)
        {
            if (transport != null) {
                this.Id = transport.Id;
                this.Login = transport.Login;
                this.Password = transport.Password;
                this.Token = transport.Token;
            }
        }
    }
}
