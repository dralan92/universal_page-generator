namespace UniversalPageGenerator.Services
{
    public interface ICrossPageSqlService
    {
        string GenerateExpandedSQL(string shortCode, string siloName);
    }
}