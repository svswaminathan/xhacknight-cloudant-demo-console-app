using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudantClient.PCL.Models
{
    public class Hackathon
    {
        public string Organiser { get; set; }
        public string Venue { get; set; }
        public DateTime Time { get; set; }
        public List<string> Sponsors{ get; set; }
    }
}
