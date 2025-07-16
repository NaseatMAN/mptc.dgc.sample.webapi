using mptc.dgc.sample.application.Stratergies;
using Xunit;

namespace mptc.dgc.sample.test
{
    public class ImportExcelUnitTest
    {
        
        [Fact]
        public void TestImportExcelForVtCompany()
        {
            var factory = new ImportStrategyFactory();
            var strategy = factory.ImportExcel("VT");
            Assert.NotNull(strategy);
            Assert.IsType<VrakbunthumImportStrategy>(strategy);
        }

        [Fact]
        public void TestImportExcelForJtCompany()
        {
            var factory = new ImportStrategyFactory();
            var strategy = factory.ImportExcel("JT");
            Assert.NotNull(strategy);
            Assert.IsType<JtCompanyImportStrategy>(strategy);
        }
    }
}
