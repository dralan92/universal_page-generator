using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalPageGenerator.Services
{
    public class SiloService : ISiloService
    {
        private string _silo;
        private string _siloJson;
        private Dictionary<string, object> _siloJsonDict;
        private List<string> _siloItemList;
        public void ProcessSilo(KeyValuePair<string,object> dictEntry)
        {
            _silo = dictEntry.Key;
            _siloJson = JsonConvert.SerializeObject(dictEntry.Value);
            _siloJsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(_siloJson);
            _siloItemList = _siloJsonDict.Keys.ToList();

            var crossList = SiloItemCrossList(_siloItemList);

            foreach(var siloItem in _siloItemList)
            {
                var crossListForTheItem = GetCrossItems(siloItem, crossList);
                foreach(var crossItem in crossListForTheItem)
                {
                    File.AppendAllText(@"GeneratedPageList.txt", crossItem + Environment.NewLine);

                }
                File.AppendAllText(@"GeneratedPageList.txt", siloItem + Environment.NewLine);
            }
        }


        public List<string> SiloItemCrossList(List<string> siloItemList)
        {
            var crossList = new List<string>();  
            for(int i=0;i< siloItemList.Count(); i++)
            {
                for(int j=i+1; j< siloItemList.Count(); j++)
                {
                    var tempString = siloItemList[i] + "-versus-vs-compare-" + siloItemList[j];
                    crossList.Add(tempString);
                }
            }
            return crossList;
        }

        public List<string> GetCrossItems(string siloItem, List<string> crossList)
        {
            var resultCrossList = new List<string>();
            foreach(var cross in crossList)
            {
                if (cross.Contains(siloItem))
                {
                    resultCrossList.Add(cross);
                }
            }
            return resultCrossList;
        }
    }
}
