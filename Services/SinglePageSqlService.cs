using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalPageGenerator.Services
{
    public class SinglePageSqlService : ISinglePageSqlService
    {
        public string GenerateExpandedSQL(string shortCode, string siloName, List<string> crossShortCodes)
        {
            File.AppendAllText(@"GeneratedPageList.txt", "singleSQL for " + shortCode + Environment.NewLine);
            return "singleSQL for " + shortCode;
        }
    }
}
