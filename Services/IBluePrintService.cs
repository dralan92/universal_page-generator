using System.Collections.Generic;

namespace UniversalPageGenerator.Services
{
    public interface IBluePrintService
    {
        List<string> GetSiloList();

        void GenerateBluePrint();
        Dictionary<string, object> ReadBluePrint();
    }
}