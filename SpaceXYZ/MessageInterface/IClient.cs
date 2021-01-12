using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessageInterface
{
    public interface IClient
    {
        [OperationContract]
        void PlaceHolder();


        [OperationContract]
        void GetMessage(string message, string spacecraftname);


        [OperationContract]
        // 0 is login and 1 is logoff
        void GetUpdate(int value, string spacecraftname);
    }
}
