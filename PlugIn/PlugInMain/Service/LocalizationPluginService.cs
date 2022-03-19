using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugInMain.Service
{
    public class LocalizationPluginService
    {
        public bool AddOrUpdatePluginResourceAsync(string key, string value, bool isStatus)
        {
            List<PluginModel> cache = new List<PluginModel>();

            using (StreamReader r = new StreamReader(@"H:\Program\Library.Net\PlugIn\PlugInMain\Service\plugin.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<PluginModel>>(json);

                foreach (var item in items)
                {
                    if (item.Name == key && item.Values == value)
                    {
                        item.status = isStatus.ToString();

                        cache.Add(new PluginModel
                        {
                            Name = item.Name,
                            Values = item.Values,
                            status = isStatus.ToString()
                        });
                    }
                    else
                    {
                        cache.Add(new PluginModel
                        {
                            Name = key,
                            Values = value,
                            status = isStatus.ToString()
                        });
                    }
                }
            }


            string output = Newtonsoft.Json.JsonConvert.SerializeObject(cache, Newtonsoft.Json.Formatting.Indented);
            
            if (string.IsNullOrEmpty(output))
            {
                File.WriteAllText(@"H:\Program\Library.Net\PlugIn\PlugInMain\Service\plugin.json", output);

                return true;
            }

            return true;
        }

        public List<PluginModel> GetPluginResourceAsync(string key)
        {
            using (StreamReader r = new StreamReader(@"H:\Program\Library.Net\PlugIn\PlugInMain\Service\plugin.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<PluginModel>>(json);

                return items.Where(n => n.Name == key).ToList();
            }
        }
    }
}
