using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugInMain.Service
{
    public class PluginService : IPlugin
    {
        public void DoProcess()
        {
            throw new NotImplementedException();
        }

        public Task InstallAsync()
        {
            throw new NotImplementedException();
        }

        public Task UninstallAsync()
        {
            throw new NotImplementedException();
        }
    }
}
