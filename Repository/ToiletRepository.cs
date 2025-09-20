using CleanMonitor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CleanMonitor.Repository
{
    public class ToiletRepository
    {
        private readonly string filePath;

        public ToiletRepository()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appData, "CleanMonitor");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            filePath = Path.Combine(folder, "toiletStatuses.json");
        }

        public void SaveAll(List<ToiletStatus> status)
        {
            var json = JsonConvert.SerializeObject(status,Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public List<ToiletStatus> LoadAll()

        {
            if (!File.Exists(filePath)) return new List<ToiletStatus>();
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ToiletStatus>>(json) ?? new List<ToiletStatus>(); ;
        }

    }
}
