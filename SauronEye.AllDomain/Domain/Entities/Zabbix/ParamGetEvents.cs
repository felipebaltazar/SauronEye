using System.Collections.Generic;

namespace SauronEye.AllDomain.Domain.Entities.Zabbix
{
    public class ParamGetEvents
	{
        public ParamGetEvents()
        {
            eventids = "4793419";
            output = "extend";
            select_acknowledges = "extend";
            selectTags = "extend";
            selectSuppressionData = "extend";
            sortfield = new List<string>() { "clock", "eventid" };
            sortorder = "DESC";
        }

        private string eventids { get; set; }
        private string output { get; set; }
        private string select_acknowledges { get; set; }
        private string selectTags { get; set; }
        private string selectSuppressionData { get; set; }
        private List<string> sortfield { get; set; }
        private string sortorder { get; set; }
    }
}
