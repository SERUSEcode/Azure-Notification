using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppRazorMAUI.Data
{
    public class Situation
    {
        public string Id { get; set; }

        public string IconId { get; set; }

        public string Message { get; set; }

        public string MessageCode { get; set; }

        public string MessageType { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
