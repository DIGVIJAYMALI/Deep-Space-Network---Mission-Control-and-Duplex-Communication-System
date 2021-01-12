using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MessageInterface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMessagingService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IMessagingService
    {
        [OperationContract]
        void test(String value);


        /*
         * Service That is used while logging into the service
         */
        [OperationContract]
        int Login(string SpacecraftName, string SpacecraftOrbit);


        /*
         * Send Message to All Method 
         * Spacecraft doesnt want to send message to itself so pass username
         */
        [OperationContract]
        void SendMessage(string message, string SpacecraftName);


        /*
       * Logout Contract
       */
        [OperationContract]
        void Logout();


        /*
      * GEt Current Logged in users
      */
        [OperationContract]
        List<string> GetCurrentUsers();


        /*
    * GEt All Current Logs
    */
        [OperationContract]
        StringBuilder GetAllLogs();

        /*
       * Request Telementry
       */
        [OperationContract]
        string RequestTelemetry(string spacecraft);

        /*
     * Request Telementry
     */
        [OperationContract]
         void LogoutByServer(string Spacecarft);
        

        /*
Update Altitude
*/
        
        [OperationContract]
        void UpdateAltitude(string Spacecraft, int timer);
        
    }
    }
