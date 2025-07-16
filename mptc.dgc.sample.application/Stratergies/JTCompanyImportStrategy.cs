using mptc.dgc.sample.application.Interfaces;

namespace mptc.dgc.sample.application.Stratergies
{
    public class JtCompanyImportStrategy : IImportStrategy
    {
        public async Task ImportAsync(Stream fileStream)
        {
            Console.WriteLine("TestCompanyImportStrategy: Importing data from the provided file stream...");
            await Task.CompletedTask;
        }
    }
}
