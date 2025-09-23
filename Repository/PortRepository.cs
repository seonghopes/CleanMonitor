using CleanMonitor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMonitor.Repository
{
    public class PortRepository
    {
        private readonly string filePath;
        public PortRepository() 
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appData, "CleanMonitor");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            filePath = Path.Combine(folder, "portMappings.json");
        }

        public void SaveAll(List<ToiletPort> mappings)
        {
            string json = JsonConvert.SerializeObject(mappings, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public List<ToiletPort> LoadAll()
        {
            if (!File.Exists(filePath)) return new List<ToiletPort>();
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ToiletPort>>(json) ?? new List<ToiletPort>();
        }
    }
}
