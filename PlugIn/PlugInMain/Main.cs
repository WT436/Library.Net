using PlugInMain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace PlugInMain
{
    public static class Main
    {
        private static IDictionary<string, IPlugin> Plugins = new Dictionary<string, IPlugin>();
        private static string PathLoadPlugIn = @"H:\Program\Library.Net\PlugIn\PlugInMain\Plugins\PlugInService_A";
        public static void StartUp()
        {
            Console.WriteLine("Start load Plugin");

            LoadPlugin();

            foreach (var key in Plugins.Keys)
            {
                Plugins[key].DoProcess();
            }

            Console.WriteLine("Stop load Plugin");
        }

        private static void LoadPlugin()
        {
            LocalizationPluginService localizationPluginService = new LocalizationPluginService();
            var data = localizationPluginService.GetPluginResourceAsync("IPlugin");

            foreach (var dll in Directory.GetFiles(PathLoadPlugIn, "*.dll"))
            {
                AssemblyLoadContext assemblyLoadContext = new AssemblyLoadContext(dll);
                Assembly asm = assemblyLoadContext.LoadFromAssemblyPath(dll);

                Type[] types = asm.GetTypes();
                foreach (Type type in types)
                {
                    Type? typeExample = type.GetInterface("IPlugin");
                    if (typeExample == null)
                    {
                        continue;
                    }

                    if (data.Any(n => n.Values == typeExample.FullName.ToString()))
                    {
                        IPlugin? plugin = asm.CreateInstance(type.FullName) as IPlugin;
                        // ở đây cài một hàm để đọc file json của nó nữa là dk!
                        if (plugin != null)
                        {
                            Plugins.Add(Path.GetFileNameWithoutExtension(dll), plugin);

                            break;
                        }
                    }
                }
            }

        }
    }
}
