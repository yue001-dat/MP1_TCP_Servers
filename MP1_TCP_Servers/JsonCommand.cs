using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP1_TCP_Servers
{
    public class JsonCommand
    {
        // {"Method": "Random", "Value1": 10, "Value2": 20}
        public string Method { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }
}
