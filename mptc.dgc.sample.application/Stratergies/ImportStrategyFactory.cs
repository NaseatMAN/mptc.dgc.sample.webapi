using mptc.dgc.sample.application.Interfaces;

namespace mptc.dgc.sample.application.Stratergies
{
    public class ImportStrategyFactory
    {
        public IImportStrategy ImportExcel(string company)
        {
            return company switch
            {
                "VT" => new VrakbunthumImportStrategy(),
                "JT" => new JtCompanyImportStrategy(),
                _ => throw new ArgumentException("Invalid company specified")
            };
        }
    }
}
