using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UniversalPageGenerator.Models;

namespace UniversalPageGenerator.Services
{
    public class SiloService : ISiloService
    {
        ISinglePageSqlService _singlePageSqlService;
        ICrossPageSqlService _crossPageSqlService;

        private string _silo;
        private string _siloJson;
        private Dictionary<string, object> _siloJsonDict;
        private List<string> _siloItemList;
        private List<string> _createdPages;
        private List<PageSql> _crossPageSqls;
        private List<PageSql> _pageSqls;

        public SiloService(ISinglePageSqlService singlePageSqlService, ICrossPageSqlService crossPageSqlService)
        {
            _singlePageSqlService = singlePageSqlService;
            _crossPageSqlService = crossPageSqlService;
        }
        public void ProcessSilo(KeyValuePair<string,object> dictEntry)
        {
            _silo = dictEntry.Key;
            _siloJson = JsonConvert.SerializeObject(dictEntry.Value);
            _siloJsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(_siloJson);
            _siloItemList = _siloJsonDict.Keys.ToList();
            var crossList = SiloItemCrossList(_siloItemList);

            _pageSqls = new List<PageSql>();
            _crossPageSqls = new List<PageSql>();

            foreach (var cross in crossList)
            {
                _crossPageSqls.Add(new PageSql {PageShortCode = cross, ExpandedSql = _crossPageSqlService.GenerateExpandedSQL(cross, _silo) });
            }
            foreach (var siloItem in _siloItemList)
            {
                var crossShortCodes = GetCrossItems(_silo, crossList);
                _pageSqls.Add(new PageSql { PageShortCode = siloItem, ExpandedSql = _singlePageSqlService.GenerateExpandedSQL(siloItem, _silo, crossShortCodes) });
            }

            //foreach (var siloItem in _siloItemList)
            //{
            //    var crossListForTheItem = GetCrossItems(siloItem, crossList);
            //    foreach(var crossItem in crossListForTheItem)
            //    {
            //        var entry = _pageSqls.Where(p => p.PageShortCode == crossItem).FirstOrDefault();
            //        if(entry == null)
            //        {
            //            _pageSqls.Add(new PageSql { PageShortCode = crossItem , ExpandedSql="SQL"});
            //        }
            //        //File.AppendAllText(@"GeneratedPageList.txt", crossItem + Environment.NewLine);

                //    }
                //    _pageSqls.Add(new PageSql { PageShortCode = siloItem, ExpandedSql = "SQL" });

                //    //File.AppendAllText(@"GeneratedPageList.txt", siloItem + Environment.NewLine);
                //}



        }

        void GetCrossFiles(string crossString)
        {
            var items = crossString.Split("-versus-vs-compare-");
            if(items.Count() == 2)
            {
                var siloImage1 = "./siloDb/" + _silo + "/" + items[1]+".jpg";
                var siloImage2 = "./siloDb/" + _silo + "/" + items[2]+".jpg";

                var siloData1 = "./siloDb/" + _silo + "/" + items[1] + ".json"; ;
                var siloData2 = "./siloDb/" + _silo + "/" + items[2] + ".json"; ;

                var crossTemplate = "./siloDb/" + _silo + "/Template/CrossTemplate.txt";

                File.AppendAllText(@"GeneratedPageList.txt", "CrossPageCreated for" + Environment.NewLine);

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
