using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonSaveAcc.actionmain
{

    public delegate void ErrorHandle(Object err, object sender, int code);
    public delegate void ProcessHandle(Object obj,object sender);
    public delegate void SuccessHandle(Object obj, object sender);
    public delegate void ActionHandle(Object obj, String data);
    public delegate void ProxyConnectHandle(Object obj, String data);
    public delegate void Action(object data, object sender);
    public delegate Object StartActionDriver(object data = null, object sender = null);
    public class ConstAll
    {
        
    }
}
