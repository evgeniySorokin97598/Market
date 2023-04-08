using Market.ConfigLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Market.ConfigLib.Logic
{
    public class FileLoader : IConfigLoader
    {
        private string _filePath;
        private JsonSerializerOptions options;

        public FileLoader(string filePath)
        {
            _filePath = filePath;
        }
        public FileLoader()
        {
            string directory = Path.Combine(Directory.GetCurrentDirectory(), "Configs");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            _filePath = Path.Combine(directory, "Config.txt");

        }

        public object Config { set; private get; }

        public object Load<T>()
        {

            return JsonSerializer.Deserialize<T>(File.ReadAllText(_filePath));
        }

        public void Save()
        {
           
            string json = JsonSerializer.Serialize(Config, options);
            File.WriteAllText(_filePath, json);
        }
    }
}
