using System.Collections.Generic;

namespace UniversalPageGenerator.Services
{
    public interface ISiloService
    {
        void ProcessSilo(KeyValuePair<string, object> dictEntry);
    }
}