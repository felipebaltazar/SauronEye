using System.Collections.Generic;

namespace SauronEye.AllDomain.Domain.Entities.Zabbix
{
    public class Problem
    {
        public string eventid { get; set; }
        public string source { get; set; }
        public string @object { get; set; }
        public string objectid { get; set; }
        public string clock { get; set; }
        public string ns { get; set; }
        public string r_eventid { get; set; }
        public string r_clock { get; set; }
        public string r_ns { get; set; }
        public string correlationid { get; set; }
        public string userid { get; set; }
        public string name { get; set; }
        public string acknowledged { get; set; }
        public string severity { get; set; }
        public string opdata { get; set; }
        public List<dynamic> acknowledges { get; set; }
        public List<object> suppression_data { get; set; }
        public string suppressed { get; set; }
        public List<Url> urls { get; set; }
        public List<object> tags { get; set; }

        public Entities.Problem GetEntity(string username)
        {
            var item = new Entities.Problem()
            {
                Eventid = eventid,
                Source = source,
                @Object = @object,
                Objectid = objectid,
                Clock = clock,
                Ns = ns,
                R_eventid = r_eventid,
                R_clock = r_clock,
                R_ns = r_ns,
                Correlationid = correlationid,
                Userid = userid,
                Name = name,
                Acknowledged = acknowledged,
                Severity = severity,
                Opdata = opdata,
                //Acknowledges = BsonArray.Create(acknowledges),
                //Suppression_data = BsonArray.Create(suppression_data),
                Suppressed = suppressed,
                Urls = urls,
                //Tags = BsonArray.Create(tags),
                Id = $"{username}_{eventid}" 
            };

            return item;
        }
    }
}
