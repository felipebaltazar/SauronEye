namespace SauronEye.AllDomain.Domain.Entities.Zabbix
{
    public class Caller
    {
        public Caller()
        {
            this.jsonrpc = "2.0";
            this.method = "user.login";
            this.id = 1;
            this.auth = null;
        }
        public string jsonrpc { get; set; }
        public string method { get; set; }
        public object @params { get; set; }
        public int id { get; set; }
        public string auth { get; set; }
    }
}
