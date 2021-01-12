using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceNetwork
{
    public class Payload
    {

        public string Name { get; set; }
        public string Type { get; set; }
        
        public Payload(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
    
    }
}
