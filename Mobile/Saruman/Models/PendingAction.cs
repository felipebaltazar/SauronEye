using System;
using Saruman.Helpers.Enums;

namespace Saruman.Models
{
    public class PendingAction
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public Priority Priority { get; set; }
    }
}
