using System;
namespace SauronEye.MasterRing.Api.Model
{
    public class PushSubscription
    {
        public string Platform { get; set; }

        public string FcmTokem { get; set; }
        public string Username { get; set; }

    }
}
