using stc.api.edu.unittest;
using stc.business.edu.Services.Interfaces;
using Xunit;

namespace stc.api.edu.UnitTest
{
    public class BranchUnitTest
    {
        private Core.Common.ContainerManager container = null;
        public BranchUnitTest()
        {
            container = ServerProvider.GetEngine();
        }

        [Fact]
        public async void ReadAll()
        {
            IBranchService _objService = container.Resolve<IBranchService>();
            var result = await _objService.ReadAll();
            Assert.True(result.Data != null, result.ErrorMessage);
        }
    }
}
