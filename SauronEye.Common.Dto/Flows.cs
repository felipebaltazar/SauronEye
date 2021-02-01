using System;

namespace SauronEye.Common.Dto
{
    public class Flows
    {
        public Flows()
        {
        }

        public string Name { get; set; }
        public DateTime LastExecution { get; set; }
        public bool Status { get; set; }
    }
}
