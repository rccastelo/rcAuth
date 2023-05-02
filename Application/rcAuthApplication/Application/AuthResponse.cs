using rcAuthDomain;
using System.Collections.Generic;

namespace rcAuthApplication.Application
{
    public class AuthResponse
    {
        public bool Valid { get; set; }
        public bool Error { get; set; }
        public IList<string> Messages { get; set; }
        public AuthEntity Item { get; set; }
        public IList<AuthEntity> List { get; set; }

        public AuthResponse()
        {
            this.Valid = true;
            this.Error = false;
        }

        public AuthResponse(AuthResponse response)
        {
            if (response != null)
            {
                this.Valid = response.Valid;
                this.Error = response.Error;

                if (response.Messages != null)
                {
                    this.Messages = new List<string>(response.Messages);
                }

                if (response.List != null)
                {
                    this.List = new List<AuthEntity>(response.List);
                }

                if (response.Item != null)
                {
                    this.Item = response.Item;
                }
            }
        }

        public void AddMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (this.Messages == null)
                {
                    this.Messages = new List<string>();
                }

                this.Messages.Add(message);
            }
        }

        public void AddEntity(AuthEntity entity)
        {
            if (entity != null)
            {
                if (this.List == null)
                {
                    this.List = new List<AuthEntity>();
                }

                this.List.Add(entity);
            }
        }
    }
}
