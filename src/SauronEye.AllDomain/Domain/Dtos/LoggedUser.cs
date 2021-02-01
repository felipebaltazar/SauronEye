using System;

namespace SauronEye.AllDomain.Domain.Dtos
{
    public class LoggedUser
    {
        public string Username { get; set; }
        public DateTime LastCall { get; set; }
    }
}
