using stc.api.edu.unittest;
using stc.business.edu.Services.Interfaces;
using stc.business.edu.Services.Interfaces;
using Xunit;

namespace stc.api.edu.UnitTest
{
    public class CountryUnitTest
    {
        private Core.Common.ContainerManager container = null;
        public CountryUnitTest()
        {
            container = ServerProvider.GetEngine();
        }

        [Fact]
        public async void ReadAll()
        {
            ICountryService _objService = container.Resolve<ICountryService>();
            var result = await _objService.ReadAll();
            Assert.True(result.Data != null, result.ErrorMessage);
        }
    }
}
