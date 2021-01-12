using MessageInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections.Concurrent;


namespace MessagingServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessagingService" in both code and config file together.
    /*
     
    InstanceContextMode : Single : one instance of service and all clients gonna share that
    we are holding list of all clinets and we are gonna look who are connected

    ConcurrencyMode : Multiple : Multithreaded Server and having multiple client handling with separate thread
    (
    But we need to do thread safety as clinets access shared resources hence need to do basic thread safety using asynchronuos thread safety basic 
    thread safety. We mostly use concurrent connection mode as it has inbuilt thread safety
    )
     */
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class MessagingService : IMessagingService
    {
        /*
        WE NEED SOME KIND OF DATA STRUCTURE TO MANAGE THE CONNECTED CLIENTS
        This Will Manage all spacecrafts connected to Messaging Service
        */
        public static ConcurrentDictionary<string, ConnectedClient> _connectedSpacecrafts =
            new ConcurrentDictionary<string, ConnectedClient>();

        public StringBuilder Logs = new StringBuilder();
      
    
        
        

        public void test(string value)
        {
            Console.WriteLine(value);
        }

        public int Login(string SpacecraftName, string SpacecraftOrbit)
        {
            Random r = new Random();
            int longitude = r.Next(-90, 90);
            int latitude = r.Next(-180, 180);
            int altitude = 0;
            int temperatureKelvin = 340;
            int timeToOrbit = 500;

            foreach (var spacecraft in _connectedSpacecrafts)
            {
                if (spacecraft.Key.ToLower() == SpacecraftName.ToLower())
                {
                    /*
                    Some spacecraft is already logged in with the same username
                    */
                    return 1;
                }
            }
            var establishedSpacecraftConnection = OperationContext.Current.GetCallbackChannel<IClient>();
            ConnectedClient newSpacecraft = new ConnectedClient();
            newSpacecraft.connection = establishedSpacecraftConnection;
            newSpacecraft.SpaceCraftName = SpacecraftName;
            newSpacecraft.Orbit = SpacecraftOrbit;
            newSpacecraft.longitude = longitude;
            newSpacecraft.latitude = latitude;
            newSpacecraft.altitude = altitude;
            newSpacecraft.temperatureKelvin = temperatureKelvin;
            newSpacecraft.timeToOrbit = timeToOrbit;
            _connectedSpacecrafts.TryAdd(SpacecraftName, newSpacecraft);
            UpdateHelperForSpacecrafts(0, SpacecraftName);

            Console.ForegroundColor = ConsoleColor.Green;
            Logs.Append("Client login :" + newSpacecraft.SpaceCraftName.ToString() + " at" + System.DateTime.Now.ToString() + "\n");
            Console.WriteLine("Client login : {0} at {1}", newSpacecraft.SpaceCraftName, System.DateTime.Now);
            Console.WriteLine("Coonected Spacecrafts {0}", _connectedSpacecrafts.Count);
            Console.ResetColor();
            /*
            Successful Login
            */
            return 0;
        }



        public void SendMessage(string message, string SpacecraftName)
        {
            foreach (var spacecraft in _connectedSpacecrafts)
            {
                if (spacecraft.Key.ToLower() != SpacecraftName.ToLower())
                {
                    spacecraft.Value.connection.GetMessage(message, SpacecraftName);
                }
            }
            Logs.Append(SpacecraftName + " : " + message + "\n");
        }

        public void Logout()
        {
            ConnectedClient spacecraft = GetMySpacecraft();
            if (spacecraft != null)
            {

                ConnectedClient removedSpaceCraft;
                _connectedSpacecrafts.TryRemove(spacecraft.SpaceCraftName, out removedSpaceCraft);

                UpdateHelperForSpacecrafts(1, removedSpaceCraft.SpaceCraftName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Client logoff : {0} at {1}", removedSpaceCraft.SpaceCraftName, System.DateTime.Now);
                Logs.Append("Client logout : " + removedSpaceCraft.SpaceCraftName + " at " + System.DateTime.Now + "\n");
                Console.ResetColor();
            }

        }

        public ConnectedClient GetMySpacecraft()
        {
            var establishedSpacecraftConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            foreach (var spacecraft in _connectedSpacecrafts)
            {
                if (spacecraft.Value.connection == establishedSpacecraftConnection)
                {
                    // THIS MEANS THIS IS THE PERSON WHO CALLED IT. Return its value

                    return spacecraft.Value;
                }
            }

            return null;
        }

        private void UpdateHelperForSpacecrafts(int value, string spacecraftname)
        {
            foreach (var spacecraft in _connectedSpacecrafts)
            {
                if (spacecraft.Value.SpaceCraftName.ToLower() != spacecraftname.ToLower())
                {
                    spacecraft.Value.connection.GetUpdate(value, spacecraftname);
                }

            }

        }

        public List<string> GetCurrentUsers()
        {
            List<string> listofspacecrafts = new List<string>();
            foreach (var spacecraft in _connectedSpacecrafts)
            {
                listofspacecrafts.Add(spacecraft.Value.SpaceCraftName);
            }
            return listofspacecrafts;
        }

        public StringBuilder GetAllLogs()
        {
            return Logs;
        }

        public string RequestTelemetry(string spacecraft)
        {
            string ret = "";
            int orbit=0;
            Random r = new Random();
            int longitude = r.Next(-90, 90);
            int latitude = r.Next(-180, 180);
            int altitude = 0;
            int temperatureKelvin = 0;
            int timeToOrbit = 0;
          

            foreach (var craft in _connectedSpacecrafts)
            {
                if (craft.Value.SpaceCraftName.ToLower() == spacecraft.ToLower())
                {
                    orbit =Convert.ToInt32(craft.Value.Orbit.ToLower());
                    craft.Value.longitude = r.Next(-90, 90); ;
                    craft.Value.latitude = r.Next(-180, 180);
                    craft.Value.altitude += 1;
                    craft.Value.temperatureKelvin -= 2;
                    craft.Value.timeToOrbit -= 1;
                    longitude = craft.Value.longitude;
                    latitude = craft.Value.latitude;
                    altitude = craft.Value.altitude;
                    temperatureKelvin = craft.Value.temperatureKelvin;
                    timeToOrbit = craft.Value.timeToOrbit;
                    break;
                }
            }

            Dictionary<string, string> RealTimeData = new Dictionary<string, string>();


            RealTimeData.Add("altitude", altitude.ToString());
            RealTimeData.Add("longitude", longitude.ToString());
            RealTimeData.Add("latitude", latitude.ToString());
            RealTimeData.Add("temperature", temperatureKelvin.ToString());
            RealTimeData.Add("timeToOrbit", timeToOrbit.ToString());

            var entries = RealTimeData.Select(d =>
        string.Format("\"{0}\": {1}\n", d.Key, string.Join(",", d.Value)));

            if(orbit <= altitude)
            {
                return "ORBIT REACHED";
            }
            return "{\n" + string.Join(",", entries) + "\n}";
        }


        public void LogoutByServer(string Spacecarft)
        {

            if (Spacecarft != null)
            {

                ConnectedClient removedSpaceCraft;
                _connectedSpacecrafts.TryRemove(Spacecarft, out removedSpaceCraft);

                UpdateHelperForSpacecrafts(1, removedSpaceCraft.SpaceCraftName);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Client logoff : {0} at {1}", removedSpaceCraft.SpaceCraftName, System.DateTime.Now);
                Logs.Append("Client logout : " + removedSpaceCraft.SpaceCraftName + " at " + System.DateTime.Now + "\n");
                Console.ResetColor();
            }

        }

        
        public void UpdateAltitude(string Spacecraft, int timer)
        {
            _connectedSpacecrafts[Spacecraft].altitude = timer;
        }
       
    }
}

/*
 * SortByColumns(Filter(
    Documents,
    'Folder path' = "Shared Documents/Qlik Dashboards - Prod Documentation/One Pager - Summary/" && StartsWith(Name, txtDashboardNameSearchPdf.Text)
),"{Name}");

 */