using System;
using Saruman.Helpers.Enums;

namespace Saruman.Models
{
    public class Incident
    {
        public string IncidentId { get; set; }
        public Priority Priority { get; set; }
        public bool Ack { get; set; }
        public string Squad { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
