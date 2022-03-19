using PlugInMain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugInService_A
{
    public class PluginService_AMethod : IPlugin
    {

        public void DoProcess()
        {
            LocalizationPluginService localizationPluginService = new LocalizationPluginService();
            localizationPluginService.AddOrUpdatePluginResourceAsync("", "",true);
            Console.WriteLine("PluginService_A run!");
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
