using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugInMain.Service
{
    public interface IPlugin
    {
        /// <summary>
        /// Process file
        /// </summary>
        public void DoProcess();
        /// <summary>
        /// Install plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public Task InstallAsync();

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public Task UninstallAsync();
    }
}
