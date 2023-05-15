using System;

namespace rcAuthDomain.Entities
{
    [Serializable]
    public class AuthEntity
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Secret { get; set; }

        public string Token { get; set; }

        public AuthEntity() { }

        public AuthEntity(AuthEntity entity) 
        {
            if (entity != null) {
                this.Id = entity.Id;
                this.Login = entity.Login;
                this.Password = entity.Password;
                this.Secret = entity.Secret;
                this.Token = entity.Token;
            }
        }
    }
}
