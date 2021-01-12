using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessagingServer
{
    class Program
    {
        
        static void Main(string[] args)
        {
           MessagingService _server= new MessagingService();
            using(ServiceHost host =  new ServiceHost(_server))

            {
                
                host.Open();
                Console.WriteLine("Server is running .....");
                Console.ReadLine();
            }


        }
    }
}
