using rcAuthDomain.Entity;
using rcUtils;
using System;
using System.Collections.Generic;

namespace rcAuthDomain.Model
{
    public class AuthModel
    {
        private AuthEntity _item = null;
        private IList<AuthEntity> _list = null;
        private IList<string> _messages = null;

        public AuthModel() { }

        public AuthModel(AuthModel model)
        {
            if (model != null) {
                this._item = model._item == null ? null : new AuthEntity(model._item);
                this._list = model._list == null ? null : new List<AuthEntity>(model._list);
                this.IsValid = model.IsValid;
                this._messages = model._messages == null ? null : new List<string>(model._messages);
            }
        }

        public AuthModel(AuthEntity entity) 
        {
            if (entity != null) this.SetItem(entity);
        }

        public AuthModel(long id, string login, string password, string token)
        {
            this.SetItem(id, login, password, token);
        }

        public AuthEntity Item
        {
            get { return this._item; }
            private set { }
        }

        public IList<AuthEntity> List
        {
            get { return this._list; }
            private set { }
        }

        public bool IsValid { get; set; }

        public IList<string> Messages
        {
            get { return this._messages; }
            private set { }
        }

        public bool ValidModel
        {
            get { return this.ValidateModel(); }
            private set { }
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

                foreach (string m in messages) {
                    if (!String.IsNullOrWhiteSpace(m)) this._messages.Add(m);
                }
            }
        }

        public void AddEntity(AuthEntity entity)
        {
            if (entity != null) {
                if (this._list == null) this._list = new List<AuthEntity>();

                this._list.Add(entity);
            }
        }

        public void AddEntities(IList<AuthEntity> entities)
        {
            if ((entities != null) && (entities.Count > 0)) {
                this._list = entities;
            }
        }

        public void SetItem(AuthEntity entity)
        {
            if (entity != null) {
                this.SetItem(entity.Id, entity.Login, entity.Password, entity.Token);
            }
        }

        public void SetItem(long id, string login, string password, string token)
        {
            this._item = new AuthEntity() {
                Id = id,
                Login = login,
                Password = password,
                Token = token
            };
        }

        private bool ValidateModel()
        {
            bool validity = true;

            if (this._item.Id < 0) {
                validity = false;
                this.AddMessage("Campo [id] deve ser maior ou igual a zero");
            }

            if (this._item.Login == null) {
                validity = false;
                this.AddMessage("Campo [login] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(this._item.Login)) {
                    validity = false;
                    this.AddMessage("Campo [login] deve estar preenchido");
                } else {
                    if (this._item.Login.Length < 3) {
                        validity = false;
                        this.AddMessage("Campo [login] deve possuir no mínimo 3 caracteres");
                    }

                    if (!Validations.ValidateChars(this._item.Login)) {
                        validity = false;
                        this.AddMessage("Campo [login] possui caracteres inválidos");
                    }
                }
            }

            if (this._item.Password == null) {
                validity = false;
                this.AddMessage("Campo [password] não pode ser nulo");
            } else {
                if (String.IsNullOrWhiteSpace(this._item.Password)) {
                    validity = false;
                    this.AddMessage("Campo [password] deve estar preenchido");
                } else {
                    if (this._item.Password.Length < 3) {
                        validity = false;
                        this.AddMessage("Campo [password] deve possuir no mínimo 3 caracteres");
                    }

                    if (!Validations.ValidateChars(this._item.Password)) {
                        validity = false;
                        this.AddMessage("Campo [password] possui caracteres inválidos");
                    }
                }
            }

            return validity;
        }
    }
}
