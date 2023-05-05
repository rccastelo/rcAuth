using rcAuthDomain.Entity;
using rcUtils;
using System;
using System.Collections.Generic;

namespace rcAuthDomain.Model
{
    public class AuthModel : AuthEntity
    {
        public bool IsValid { get; private set; }
        public List<string> Messages { get; private set; }

        public AuthModel() 
        {
            IsValid = true;
            Messages = new List<string>();
        }

        public AuthModel(AuthModel model) 
        {
            if (model != null) {
                this.Id = model.Id;
                this.Login = model.Login;
                this.Password = model.Password;
                this.Token = model.Token;
                this.IsValid = model.IsValid;
                this.Messages = new List<string>(model.Messages);
            }
        }

        public AuthModel(long id, string login, string password, string token) : this()
        {
            this.Create(id, login, password, token);
        }

        public AuthModel(AuthEntity entity) : this()
        {
            this.Create(entity);
        }

        private void Create(AuthEntity entity)
        {
            if (entity != null) {
                this.Create(entity.Id, entity.Login, entity.Password, entity.Token);
            } else {
                this.IsValid = false;
                this.AddMessage("[Auth] não pode ser nulo");
            }
        }

        private void Create(long id, string login, string password, string token) 
        {
            if (id < 0) {
                this.IsValid = false;
                this.AddMessage("Campo [id] deve ser maior ou igual a zero");
            }

            if (login == null) {
                this.IsValid = false;
                this.AddMessage("Campo [login] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(login)) {
                    this.IsValid = false;
                    this.AddMessage("Campo [login] deve ser preenchido");
                } else {
                    if (login.Length < 3) {
                        this.IsValid = false;
                        this.AddMessage("Campo [login] deve possuir no mínimo 3 caracteres");
                    }

                    if (!Validations.ValidateChars(login))
                    {
                        this.IsValid = false;
                        this.AddMessage("Campo [login] possui caracteres inválidos");
                    }
                }
            }

            if (password == null) {
                this.IsValid = false;
                this.AddMessage("Campo [password] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(password)) {
                    this.IsValid = false;
                    this.AddMessage("Campo [password] deve ser preenchido");
                } else {
                    if (password.Length < 3) {
                        this.IsValid = false;
                        this.AddMessage("Campo [password] deve possuir no mínimo 3 caracteres");
                    }

                    if (!Validations.ValidateChars(password)) {
                        this.IsValid = false;
                        this.AddMessage("Campo [password] possui caracteres inválidos");
                    }
                }
            }

            if (this.IsValid) {
                this.Id = id;
                this.Login = login;
                this.Password = password;
                this.Token = token;
            }
        }

        public void AddMessage(string message)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                if (this.Messages == null) this.Messages = new List<string>();

                this.Messages.Add(message);
            }
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
