using MessageInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingServer
{
    /*
      THIS CLASS HOLDS THE SpaceCraft Name and Data
     */
    public class ConnectedClient
    {

        public IClient connection;
        public string SpaceCraftName { get; set; }

        public string Orbit { get; set; }
        
        public int longitude { get; set; }
        public int latitude { get; set; }
        public int altitude { get; set; }
        public int temperatureKelvin { get; set; }
        public int timeToOrbit { get; set; } 
    }
}
