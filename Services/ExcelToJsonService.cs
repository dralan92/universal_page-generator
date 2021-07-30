using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalPageGenerator.Services
{
    public class ExcelToJsonService : IExcelToJsonService
    {
        public void Convert(string file)
        {
            string inputFile = File.ReadAllText("excelData.txt");
        }
    }
}
