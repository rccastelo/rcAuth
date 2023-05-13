using rcAuthDomain.Transports;
using System;
using System.Collections.Generic;

namespace rcAuthApplication.Transport
{
    [Serializable]
    public class AuthResponse
    {
        private IList<string> _messages;
        private AuthTransport _item;
        private IList<AuthTransport> _list;

        public AuthResponse()
        {
            this.IsValid = true;
            this.IsError = false;
        }

        public AuthResponse(AuthResponse response) : this()
        {
            if (response != null) {
                this.IsValid = response.IsValid;
                this.IsError = response.IsError;

                if (response.Messages != null) {
                    this._messages = new List<string>(response.Messages);
                }

                if (response.List != null) {
                    this._list = new List<AuthTransport>(response.List);
                }

                if (response.Item != null) {
                    this._item = new AuthTransport(response.Item);
                }
            }
        }

        public bool IsValid { get; set; }
        public bool IsError { get; set; }

        public IList<string> Messages
        {
            get { return this._messages; }
        }

        public AuthTransport Item
        {
            get { return this._item; }
        }

        public IList<AuthTransport> List
        {
            get { return this._list; }
        }

        public void AddMessage(string message)
        {
            if (!string.IsNullOrEmpty(message)) {
                if (this._messages == null) {
                    this._messages = new List<string>();
                }

                this.Messages.Add(message);
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
        public void AddTransport(AuthTransport transport)
        {
            if (transport != null) {
                if (this._list == null) this._list = new List<AuthTransport>();
                this.List.Add(transport);
            }
        }

        public void AddList(IList<AuthTransport> list)
        {
            if ((list != null) && (list.Count > 0)) {
                if (this._list == null) this._list = new List<AuthTransport>();

                foreach (AuthTransport transport in list) {
                    if (transport != null) this._list.Add(transport);
                }
            }
        }

        public void SetItem(AuthTransport item)
        {
            if (item != null) this._item = item;
        }
    }
}
