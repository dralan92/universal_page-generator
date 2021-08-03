using System.Collections.Generic;

namespace UniversalPageGenerator.Services
{
    public interface ISinglePageSqlService
    {
        string GenerateExpandedSQL(string shortCode, string siloName, List<string> crossShortCodes);
    }
}