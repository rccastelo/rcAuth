using rcAuthDomain.Entity;
using System;

namespace rcAuthApplication.Transport
{
    [Serializable]
    public class AuthRequest : AuthEntity
    {
        public AuthRequest() : base() { }

        public AuthRequest(AuthRequest request) : base(request) { }
    }
}
