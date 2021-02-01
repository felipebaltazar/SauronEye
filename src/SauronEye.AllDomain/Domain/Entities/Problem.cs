using SauronEye.AllDomain.Domain.Contracts;
using SauronEye.AllDomain.Domain.Entities.Zabbix;
using System.Collections.Generic;

namespace SauronEye.AllDomain.Domain.Entities
{
    public class Problem : IEntity
    {
        public string Id { get; set; }

        public string Eventid { get; set; }

        public string Source { get; set; }

        public string @Object { get; set; }

        public string Objectid { get; set; }

        public string Clock { get; set; }

        public string Ns { get; set; }

        public string R_eventid { get; set; }

        public string R_clock { get; set; }

        public string R_ns { get; set; }

        public string Correlationid { get; set; }

        public string Userid { get; set; }

        public string Name { get; set; }
        public string Acknowledged { get; set; }

        public string Severity { get; set; }

        public string Opdata { get; set; }

        //public BsonArray Acknowledges { get; set; }
        //public BsonArray Suppression_data { get; set; }
        public string Suppressed { get; set; }
        public List<Url> Urls { get; set; }
        //public BsonArray Tags { get; set; }
    }
}
