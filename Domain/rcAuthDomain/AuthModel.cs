using System;

namespace rcAuthDomain
{
    public class AuthModel : AuthEntity
    {
        public AuthModel() { }

        public AuthModel(AuthModel model) 
        {
            if (model != null) {
                this.Id = model.Id;
                this.Login = model.Login;
                this.Password = model.Password;
                this.Token = model.Token;
            }
        }

        public AuthModel(long id, string login, string password, string token)
        {
            this.Create(id, login, password, token);
        }

        public AuthModel(AuthEntity entity)
        {
            this.Create(entity);
        }

        private void Create(AuthEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("[Auth] não pode ser nulo", "Auth");
            }

            this.Create(entity.Id, entity.Login, entity.Password, entity.Token);
        }

        private void Create(long id, string login, string password, string token) 
        {
            if (id < 0)
            {
                throw new ArgumentException("Campo [id] deve ser maior ou igual a zero", "id");
            }

            if (login == null)
            {
                throw new ArgumentException("Campo [login] deve ser informado", "login");
            }
            else
            {
                login = login.Trim();

                if (String.IsNullOrWhiteSpace(login))
                {
                    throw new ArgumentException("Campo [login] deve ser informado", "login");
                }

                if (login.Length < 3)
                {
                    throw new ArgumentException("Campo [login] deve possuir no mínimo 3 caracteres", "login");
                }
            }

            if (password == null)
            {
                throw new ArgumentException("Campo [password] deve ser informado", "password");
            }
            else
            {
                password = password.Trim();

                if (String.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("Campo [password] deve ser informado", "password");
                }

                if (password.Length < 3)
                {
                    throw new ArgumentException("Campo [password] deve possuir no mínimo 3 caracteres", "password");
                }
            }

            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Token = token;
        }

        public AuthEntity ToEntity()
        {
            return new AuthEntity()
            {
                Id = this.Id,
                Login = this.Login,
                Password = this.Password,
                Token = this.Token
            };
        }
    }
}
