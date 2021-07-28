using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            WriteSilosToJsonFile(GetSiloList());
        }
    }
}
