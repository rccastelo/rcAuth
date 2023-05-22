namespace rcAuthDomain.Entities
{
    public class LoginEntity
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Secret { get; set; }
        public long User_Id { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }

        public LoginEntity() { }

        public LoginEntity(LoginEntity entity)
        {
            if (entity != null) {
                this.Id = entity.Id;
                this.Login = entity.Login;
                this.Secret = entity.Secret;
                this.User_Id = entity.User_Id;
                this.Password = entity.Password;
                this.Confirmation = entity.Confirmation;
            }
        }
    }
}
