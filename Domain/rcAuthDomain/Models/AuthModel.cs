using rcAuthDomain.Entities;
using rcAuthDomain.Transports;
using rcCryptography;
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
        private bool _validModel = false;

        public AuthModel() { }

        public AuthModel(AuthModel model)
        {
            if (model != null) {
                this.SetEntity(model._entity);
                this.AddEntities(model._entities);
                this.IsValidResponse = model.IsValidResponse;
                this.AddMessages(model._messages);
            }
        }

        public AuthModel(AuthEntity entity) 
        {
            if (entity != null) this.SetEntity(entity);
        }

        public AuthModel(AuthRequestItem request)
        {
            if (request != null) this.SetEntity(request);
        }

        public AuthModel(long id, string login, string password, string token, string system)
        {
            this.SetEntity(id, login, password, token, system);
        }

        public AuthEntity Entity
        {
            get { return this._entity; }
        }

        public IList<AuthEntity> Entities
        {
            get { return this._entities; }
        }

        public AuthResponseItem ResponseItem
        {
            get { return this.GetResponseItem(); }
        }

        public IList<AuthResponseItem> ResponseList
        {
            get { return this.GetResponseList(); }
        }

        public bool IsValidResponse { get; set; }

        public IList<string> Messages
        {
            get { return this._messages; }
        }

        public bool IsValidModel
        {
            get { return this._validModel; }
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
                if (this._entities == null) this._entities = new List<AuthEntity>();

                foreach (AuthEntity entity in entities) {
                    if (entity != null) this._entities.Add(entity);
                }
            }
        }

        private void SetEntity(AuthEntity entity)
        {
            if (entity != null) {
                this.SetEntity(entity.Id, entity.Login, entity.Password, entity.Token, entity.System,
                        entity.DateFrom, entity.DateTo, entity.Weekday, entity.Weekend, entity.StartTime, entity.EndTime);
            }
        }

        private void SetEntity(AuthRequestItem request)
        {
            if (request != null) {
                this.SetEntity(0, request.Login, request.Password, string.Empty, request.System);
            }
        }

        private void SetEntity(long id, string login, string password, string token, string system)
        {
            this.SetEntity(id, login, password, token, system,
                        DateTime.MinValue, DateTime.MinValue, false, false, TimeSpan.Zero, TimeSpan.Zero);
        }

        private void SetEntity(long id, string login, string password, string token, string system,
                DateTime dateFrom, DateTime dateTo, bool weekday, bool weekend, TimeSpan startTime, TimeSpan endTime)
        {
            this._entity = new AuthEntity() {
                Id = id,
                Login = login,
                Password = password,
                Token = token,
                System = system,
                DateFrom = dateFrom,
                DateTo = dateTo,
                Weekday = weekday,
                Weekend = weekend,
                StartTime = startTime,
                EndTime = endTime
            };
            this.ValidateModel();
            this.GenerateSecret();
        }

        private AuthResponseItem GetResponseItem()
        {
            if (this._entity == null) {
                return null;
            } else {
                return new AuthResponseItem() {
                    Id = this._entity.Id,
                    Login = this._entity.Login,
                    Token = this._entity.Token
                };
            }
        }

        private IList<AuthResponseItem> GetResponseList()
        {
            if ((this._entities == null) || (this._entities.Count <= 0)) {
                return null;
            } else {
                return this._entities.Select(et => new AuthResponseItem() {
                    Id = et.Id,
                    Login = et.Login,
                    Token = et.Token
                }).ToList();
            }
        }

        private void GenerateSecret()
        {
            if ((this._entity != null) &&
                ((!string.IsNullOrWhiteSpace(this._entity.Login)) &&
                (!string.IsNullOrWhiteSpace(this._entity.Password))))
            {
                this._entity.Secret = Crypto.GetSecretSHA512(this._entity.Login + this._entity.Password);
            }
        }

        public void SetToken(string token)
        {
            this._entity.Token = token;
        }

        private void ValidateModel()
        {
            bool validity = true;
            this._messages = null;

            if (this._entity.Id < 0) {
                validity = false;
                this.AddMessage("Campo [Id] deve ser maior ou igual a zero");
            }

            if (this._entity.Login == null) {
                validity = false;
                this.AddMessage("Campo [Login] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(this._entity.Login)) {
                    validity = false;
                    this.AddMessage("Campo [Login] deve estar preenchido");
                } else {
                    if ((this._entity.Login.Length < 3) || (this._entity.Login.Length > 30)) {
                        validity = false;
                        this.AddMessage("Campo [Login] deve possuir entre 3 e 30 caracteres");
                    }

                    if (!Validations.ValidateChars_Login(this._entity.Login)) {
                        validity = false;
                        this.AddMessage("Campo [Login] possui caracteres inválidos");
                        this.AddMessage("Caracteres válidos...");
                        this.AddMessage(Validations.ValidChars_Login);
                    }
                }
            }

            if (this._entity.Password == null) {
                validity = false;
                this.AddMessage("Campo [Password] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(this._entity.Password)) {
                    validity = false;
                    this.AddMessage("Campo [Password] deve estar preenchido");
                } else {
                    if ((this._entity.Password.Length < 3) || (this._entity.Password.Length > 30)) {
                        validity = false;
                        this.AddMessage("Campo [Password] deve possuir entre 3 e 30 caracteres");
                    }

                    if (!Validations.ValidateChars_Password(this._entity.Password)) {
                        validity = false;
                        this.AddMessage("Campo [Password] possui caracteres inválidos");
                        this.AddMessage("Caracteres válidos...");
                        this.AddMessage(Validations.ValidChars_Password);
                    }
                }
            }

            if (this._entity.System == null) {
                validity = false;
                this.AddMessage("Campo [System] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(this._entity.System)) {
                    validity = false;
                    this.AddMessage("Campo [System] deve estar preenchido");
                } else {
                    if ((this._entity.System.Length < 3) || (this._entity.System.Length > 30)) {
                        validity = false;
                        this.AddMessage("Campo [System] deve possuir entre 3 e 30 caracteres");
                    }

                    if (!Validations.ValidateChars_Name(this._entity.System)) {
                        validity = false;
                        this.AddMessage("Campo [System] possui caracteres inválidos");
                        this.AddMessage("Caracteres válidos...");
                        this.AddMessage(Validations.ValidChars_Name);
                    }
                }
            }

            this._validModel = validity;
        }
    }
}
