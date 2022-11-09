using System;

namespace ApiPushClient
{


    public class Rootobject
    {
        public RESPONSE RESPONSE { get; set; }
    }

    public class RESPONSE
    {
        public RESULT[] RESULT { get; set; }
    }

    public class RESULT
    {
        public Trainannouncement[] TrainAnnouncement { get; set; }
        public INFO INFO { get; set; }
    }

    public class INFO
    {
        public string SSEURL { get; set; }
    }

    public class Trainannouncement
    {
        public string ActivityType { get; set; }
        public string LocationSignature { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime TimeAtLocation { get; set; }
    }


}
