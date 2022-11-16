using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServerSideAPI.Model
{
	[Keyless]
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
        [Key]
		public string? Id { get; set; }
		public string? IconId { get; set; }
        public string? Message { get; set; }
        public string? MessageCode { get; set; }
        public string? MessageType { get; set; }
		public DateTime CreationTime { get; set; }
	}
}
