using rcAuthDomain.Transports;
using System;
using System.Collections.Generic;

namespace rcAuthApplication.Transport
{
    [Serializable]
    public class AuthResponse
    {
        private IList<string> _messages;
        private AuthResponseItem _item;
        private IList<AuthResponseItem> _list;

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
                this.AddMessages(response.Messages);
                this.AddList(response.List);
                this.SetItem(response.Item);
            }
        }

        public bool IsValid { get; set; }
        public bool IsError { get; set; }

        public IList<string> Messages
        {
            get { return this._messages; }
        }

        public AuthResponseItem Item
        {
            get { return this._item; }
        }

        public IList<AuthResponseItem> List
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

        public void AddItem(AuthResponseItem item)
        {
            if (item != null)
            {
                if (this._list == null) this._list = new List<AuthResponseItem>();
                this.List.Add(item);
            }
        }

        public void AddList(IList<AuthResponseItem> list)
        {
            if ((list != null) && (list.Count > 0)) {
                if (this._list == null) this._list = new List<AuthResponseItem>();

                foreach (AuthResponseItem item in list) {
                    if (item != null) this._list.Add(item);
                }
            }
        }

        public void SetItem(AuthResponseItem item)
        {
            if (item != null) this._item = item;
        }
    }
}
