using SauronEye.AllDomain.Domain.Contracts;
using System.Collections.Generic;

namespace SauronEye.AllDomain.Domain.Entities
{
    public class User : IEntity
    {
        public User()
        {
            this.Profiles = new List<string>();
            this.Areas = new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TokenZabbix { get; set; }
        public string IdZabbix { get; set; }
        public string TokenFirebase { get; set; }
        public List<string> Areas { get; internal set; }

        public bool TryAddArea(string area)
        {
            if (this.Areas != null && this.Areas.Contains(area))
            {
                return false;
            }
            this.Areas.Add(area);
            return true;
        }

        public List<string> Profiles { get; internal set; }
        public string Jwt { get; set; }

        public bool TryAddProfile(string profile)
        {
            if (this.Profiles.Contains(profile))
            {
                return false;
            }
            this.Profiles.Add(profile);
            return true;
        }
    }
}
