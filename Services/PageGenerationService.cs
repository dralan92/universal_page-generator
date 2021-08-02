using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalPageGenerator.Services
{
    public class PageGenerationService : IPageGenerationService
    {
        IBluePrintService _bluePrintService;
        ISiloService _siloService;
        public PageGenerationService(IBluePrintService bluePrintService, ISiloService siloService)
        {
            _bluePrintService = bluePrintService;
            _siloService = siloService;
        }

        public void GenerateSQLForPages()
        {
            _bluePrintService.GenerateBluePrint();
            var bluePrintDict = _bluePrintService.ReadBluePrint();
            foreach(var entry in bluePrintDict)
            {
                _siloService.ProcessSilo(entry);
            }
        }

    }
}
