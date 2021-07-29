using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;

namespace UniversalPageGenerator.Services
{
    public class BluePrintService : IBluePrintService
    {
        public List<string> GetSiloList()
        {
            DirectoryInfo directory = new DirectoryInfo("./siloDb");
            DirectoryInfo[] directories = directory.GetDirectories();
            List<string> silos = new List<string>();
            foreach (DirectoryInfo folder in directories)
            {
                silos.Add(folder.Name);
            }
            return silos;
        }

        public void WriteSilosToJsonFile(List<string> siloList)
        {
            var siloDict = new Dictionary<string, string>();
            foreach(var silo in siloList)
            {
                siloDict.Add(silo, silo + "_Value");
            }
            string json = JsonConvert.SerializeObject(siloDict, Formatting.Indented);
            File.WriteAllText("bluePrint.json", json);
        }

        public void GenerateBluePrint()
        {
            //Level 1
            WriteSilosToJsonFile(GetSiloList());
            //Level 1 Complete

            //Level 2
            var silos = GetSiloList();
            foreach(var silo in silos)
            {
                var siloItems = GetSiloItems(silo);
                if (siloItems.Count() > 0)
                {
                    var siloItemDict = CreateDictionary(siloItems);
                    WriteDictionaryToFile(siloItemDict, silo);
                }
            }
            //Level 2 Completed
            
            //Level 3 
            foreach (var silo in silos)
            {
                var siloItems = GetSiloItems(silo);
                if (siloItems.Count() > 0)
                {
                    foreach(var siloItem in siloItems)
                    {
                        var path = "./siloDb/" + silo + "/" + siloItem;
                        var siloFilesDict = GetSiloItemFiles(path);
                        WriteDictionaryToFile(siloFilesDict, siloItem);
                    }
                }
            }

        }

        public List<string> GetSiloItems(string silo)
        {
            DirectoryInfo directory = new DirectoryInfo("./siloDb/"+ silo);
            DirectoryInfo[] directories = directory.GetDirectories();
            List<string> siloItems = new List<string>();
            foreach (DirectoryInfo folder in directories)
            {
                siloItems.Add(folder.Name);
            }
            return siloItems;
        }

        public Dictionary<string,string> GetSiloItemFiles(string siloItemFolder)
        {
            var siloFiles_image = Directory.GetFiles(siloItemFolder, "*.jpg").Select(s=>s.Replace("\\","/")).ToList().FirstOrDefault();
            var siloFiles_data = Directory.GetFiles(siloItemFolder, "*.json").Select(s=>s.Replace("\\","/")).ToList().FirstOrDefault();
            var siloFiles_template = Directory.GetFiles(siloItemFolder, "*.txt").Select(s=>s.Replace("\\","/")).ToList().FirstOrDefault();
            return new Dictionary<string, string>() {
                {"Image", siloFiles_image},
                { "Data", siloFiles_data},
                { "Template", siloFiles_template}
            };
        }

        public Dictionary<string, string> CreateDictionary(List<string> names)
        {
            var dict = new Dictionary<string, string>();
            foreach(var name in names)
            {
                dict.Add(name, name + "_Value");
            }
            return dict;
        }

        public void WriteDictionaryToFile(Dictionary<string, string> dict, string parent)
        {
            string json = JsonConvert.SerializeObject(dict, Formatting.Indented);
            string bluePrintContent = File.ReadAllText("bluePrint.json");
            var valueToReplace = "\"" + parent + "_Value"+"\"";
            bluePrintContent = bluePrintContent.Replace(valueToReplace, json);
            File.WriteAllText("bluePrint.json", bluePrintContent);
        }

        public Dictionary<string, object> ReadBluePrint()
        {
            var dict = new Dictionary<string, object>();
            using (StreamReader r = new StreamReader("bluePrint.json"))
            {
                string json = r.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                var receivedjson = serializer.Deserialize<Dictionary<string, string>>(json);
                var _jsonSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    TypeNameAssemblyFormat = FormatterAssemblyStyle.Full
                };
                dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, _jsonSettings);
                
            }

            return dict;
        }
    }
}
