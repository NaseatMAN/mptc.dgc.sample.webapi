using mptc.dgc.sample.application.Interfaces;

namespace mptc.dgc.sample.application.Stratergies
{
    public class VrakbunthumImportStrategy : IImportStrategy
    {
        public async Task ImportAsync(Stream fileStream)
        {
            Console.WriteLine("Importing Vrakbunthum data...");
            await Task.CompletedTask;
        }
    }
}
