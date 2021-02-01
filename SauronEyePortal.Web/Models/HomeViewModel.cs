using SauronEye.Common.Dto;
using System.Collections.Generic;

namespace SauronEye.Portal.Models
{
    public class HomeViewModel
    {
        public ICollection<Flows> FlowInProgress { get; set; }

        public ICollection<Flows> FlowNews { get; set; }

    }
}
