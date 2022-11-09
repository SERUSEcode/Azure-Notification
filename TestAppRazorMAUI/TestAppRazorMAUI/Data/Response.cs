﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppRazorMAUI.Data
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
        public Situation[] Situation { get; set; }
    }
    public class Situation
    {
        public Deviation[] Deviation { get; set; }
        
    }

    public class Deviation
    {
        public string Header { get; set; }
        public string IconId { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public string MessageType { get; set; }
    }
}
