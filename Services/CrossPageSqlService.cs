using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalPageGenerator.Services
{
    public class CrossPageSqlService : ICrossPageSqlService
    {
        public string GenerateExpandedSQL(string shortCode, string siloName)
        {
            File.AppendAllText(@"GeneratedPageList.txt", "crossSQL for " + shortCode + Environment.NewLine);
            return "crossSQL for " + shortCode;
        }
    }
}
