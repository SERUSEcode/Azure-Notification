using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServerSideAPI.Model
{

    public class Rootobject
    {
        public RESPONSE? RESPONSE { get; set; }
    }

    public class RESPONSE
    {
        public RESULT[]? RESULT { get; set; }
    }

    public class RESULT
    {
        public Situation[]? Situation { get; set; }
        public INFO? INFO { get; set; }
    }
    public class INFO
    {
        public string? LASTCHANGEID { get; set; }
    }
    public class Situation
    {
        public Deviation[]? Deviation { get; set; }

    }

    public class Deviation
    {
        public string? IconId { get; set; }
        public string? Message { get; set; }
        public string? MessageCode { get; set; }
        public string? MessageType { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
