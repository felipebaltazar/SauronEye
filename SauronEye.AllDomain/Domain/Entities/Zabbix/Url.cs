using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace SauronEye.AllDomain.Domain.Entities.Zabbix
{
    public class Url
    {
        [BsonSerializer(typeof(StringSerializer))]
        public string url { get; set; }
        [BsonSerializer(typeof(StringSerializer))]
        public string name { get; set; }
    }
}
