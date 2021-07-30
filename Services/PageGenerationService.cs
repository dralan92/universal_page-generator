using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalPageGenerator.Services
{
    public class PageGenerationService
    {
        IBluePrintService _bluePrintService;
        public PageGenerationService(IBluePrintService bluePrintService)
        {
            _bluePrintService = bluePrintService;
        }

    }
}
