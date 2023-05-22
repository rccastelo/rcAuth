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
        public string System { get; set; }
        public string Token { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool Weekday { get; set; }
        public bool Weekend { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


        public AuthEntity() { }

        public AuthEntity(AuthEntity entity) 
        {
            if (entity != null) {
                this.Id = entity.Id;
                this.Login = entity.Login;
                this.Password = entity.Password;
                this.Secret = entity.Secret;
                this.System = entity.System;
                this.Token = entity.Token;
                this.DateFrom = entity.DateFrom;
                this.DateTo = entity.DateTo;
                this.Weekday = entity.Weekday;
                this.Weekend = entity.Weekend;
                this.StartTime = entity.StartTime;
                this.EndTime = entity.EndTime;
            }
        }
    }
}
