using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalPageGenerator.Models
{
    public class Silos
    {
        public List<string> SiloList { get; set; }

    }

    public class PageSql
    {
        public string PageShortCode { get; set; }
        public string ExpandedSql { get; set; }

    }

    public class CrossPageData
    {

    }
}
