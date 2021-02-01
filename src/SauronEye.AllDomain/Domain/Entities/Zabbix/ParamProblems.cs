using System.Collections.Generic;

namespace SauronEye.AllDomain.Domain.Entities.Zabbix
{
    public class ParamProblems
    {
        public ParamProblems()
        {
			this.output = "extend";
			this.selectAcknowledges = "extend";
			this.selectTags = "extend";
			this.selectSuppressionData = "extend";
			this.sortfield = new List<string>() { "eventid" };
			this.sortorder = "DESC";
			this.severities = new List<int>() { 5 };

        }

		public string output { get; set; }
		public string selectAcknowledges { get; set; }
		public string selectTags { get; set; }
		public string selectSuppressionData { get; set; }
		public List<string> sortfield { get; set; }
		public string sortorder { get; set; }
		public List<int> severities { get; set; }

	}
}
