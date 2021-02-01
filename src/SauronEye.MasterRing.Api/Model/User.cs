using System;
using System.Text.Json.Serialization;

namespace SauronEye.MasterRing.Api.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string[] Areas { get; set; }
        public string[] Profiles { get; set; }
        
        [JsonIgnore] 
        public bool ValidEntity { get; private set; }

        [JsonIgnore] 
        public string EntityMessage { get; private set; }

        public SauronEye.AllDomain.Domain.Entities.User GetEntity()
        {
            this.ValidEntity = true;
            var user = new SauronEye.AllDomain.Domain.Entities.User
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = this.Name,
                Username = this.Username,
                Password = this.Password
                
            };

            if (this.Areas != null)
            {
                foreach (string areaName in this.Areas)
                {
                    this.ValidEntity = this.ValidEntity && user.TryAddArea(areaName);
                    if (!this.ValidEntity)
                    {
                        this.EntityMessage = $"A area {areaName} já existe\n";
                        return null;
                    }
                }
            }

            if (this.Profiles != null)
            {
                foreach (string profile in this.Profiles)
                {
                    this.ValidEntity = this.ValidEntity && user.TryAddProfile(profile);
                    if (!this.ValidEntity)
                    {
                        this.EntityMessage = $"O profile {profile} já existe\n";
                        return null;
                    }
                }
            }

            return user;
        }
    }
}
