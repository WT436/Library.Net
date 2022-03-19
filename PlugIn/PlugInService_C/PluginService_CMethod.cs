using PlugInMain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugInService_A
{
    internal class PluginService_CMethod : IPlugin
    {

        public void DoProcess()
        {
            Console.WriteLine("PluginService_C run!");
        }
    }
}
