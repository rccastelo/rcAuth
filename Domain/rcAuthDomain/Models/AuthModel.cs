using rcAuthDomain.Entities;
using rcAuthDomain.Transports;
using rcUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rcAuthDomain.Models
{
    public class AuthModel
    {
        private AuthEntity _entity = null;
        private IList<AuthEntity> _entities = null;
        private IList<string> _messages = null;

        public AuthModel() { }

        public AuthModel(AuthModel model)
        {
            if (model != null) {
                this._entity = model._entity == null ? null : new AuthEntity(model._entity);
                this._entities = model._entities == null ? null : new List<AuthEntity>(model._entities);
                this.IsValidResponse = model.IsValidResponse;
                this._messages = model._messages == null ? null : new List<string>(model._messages);
            }
        }

        public AuthModel(AuthEntity entity) 
        {
            if (entity != null) this.SetEntity(entity);
        }

        public AuthModel(AuthTransport transport)
        {
            if (transport != null) this.SetEntity(transport);
        }

        public AuthModel(long id, string login, string password, string token)
        {
            this.SetEntity(id, login, password, token);
        }

        public AuthEntity Entity
        {
            get { return this._entity; }
            private set { }
        }

        public IList<AuthEntity> Entities
        {
            get { return this._entities; }
            private set { }
        }

        public AuthTransport Transport
        {
            get { return this.GetTransport(); }
        }

        public IList<AuthTransport> TransportList
        {
            get { return this.GetListTransport(); }
        }

        public bool IsValidResponse { get; set; }

        public IList<string> Messages
        {
            get { return this._messages; }
        }

        public bool IsValidModel
        {
            get { return this.ValidateModel(); }
        }

        public void AddMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message)) {
                if (this._messages == null) this._messages = new List<string>();
                this._messages.Add(message);
            }
        }

        public void AddMessages(IList<string> messages)
        {
            if ((messages != null) && (messages.Count > 0)) {
                if (this._messages == null) this._messages = new List<string>();

                foreach (string message in messages) {
                    if (!String.IsNullOrWhiteSpace(message)) this._messages.Add(message);
                }
            }
        }

        public void AddEntity(AuthEntity entity)
        {
            if (entity != null) {
                if (this._entities == null) this._entities = new List<AuthEntity>();
                this._entities.Add(entity);
            }
        }

        public void AddEntities(IList<AuthEntity> entities)
        {
            if ((entities != null) && (entities.Count > 0)) {
                this._entities = entities;
            }
        }

        private void SetEntity(AuthEntity entity)
        {
            if (entity != null) {
                this.SetEntity(entity.Id, entity.Login, entity.Password, entity.Token);
            }
        }

        private void SetEntity(AuthTransport transport)
        {
            if (transport != null) {
                this.SetEntity(transport.Id, transport.Login, transport.Password, transport.Token);
            }
        }

        private void SetEntity(long id, string login, string password, string token)
        {
            this._entity = new AuthEntity() {
                Id = id,
                Login = login,
                Password = password,
                Token = token
            };
        }

        private AuthTransport GetTransport()
        {
            if (this._entity == null) {
                return null;
            } else {
                return new AuthTransport() {
                    Id = this._entity.Id,
                    Login = this._entity.Login,
                    Password = this._entity.Password,
                    Token = this._entity.Token
                };
            }
        }

        private IList<AuthTransport> GetListTransport()
        {
            if ((this._entities == null) || (this._entities.Count <= 0)) {
                return null;
            } else {
                return this._entities.Select(et => new AuthTransport() {
                    Id = et.Id,
                    Login = et.Login,
                    Password = et.Password,
                    Token = et.Token
                }).ToList();
            }
        }

        private bool ValidateModel()
        {
            bool validity = true;

            if (this._entity.Id < 0) {
                validity = false;
                this.AddMessage("Campo [id] deve ser maior ou igual a zero");
            }

            if (this._entity.Login == null) {
                validity = false;
                this.AddMessage("Campo [login] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(this._entity.Login)) {
                    validity = false;
                    this.AddMessage("Campo [login] deve estar preenchido");
                } else {
                    if (this._entity.Login.Length < 3) {
                        validity = false;
                        this.AddMessage("Campo [login] deve possuir no mínimo 3 caracteres");
                    }

                    if (!Validations.ValidateChars(this._entity.Login)) {
                        validity = false;
                        this.AddMessage("Campo [login] possui caracteres inválidos");
                    }
                }
            }

            if (this._entity.Password == null) {
                validity = false;
                this.AddMessage("Campo [password] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(this._entity.Password)) {
                    validity = false;
                    this.AddMessage("Campo [password] deve estar preenchido");
                } else {
                    if (this._entity.Password.Length < 3) {
                        validity = false;
                        this.AddMessage("Campo [password] deve possuir no mínimo 3 caracteres");
                    }

                    if (!Validations.ValidateChars(this._entity.Password)) {
                        validity = false;
                        this.AddMessage("Campo [password] possui caracteres inválidos");
                    }
                }
            }

            return validity;
        }
    }
}
