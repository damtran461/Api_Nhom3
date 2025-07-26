using stc.business.mce.Services.Interfaces;
using stc.dto.mce.Response;
using Core.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CountrysController : AppBaseController
    {
        private readonly ICountryService _service;

        public CountrysController(ICountryService service)
        {
            _service = service;
        }

        /// <summary>
        /// ReadAll
        /// </summary>
        /// 2023/09/22 - BaoNN -  - 20230915-MasterData
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(CountryRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadAll()
        {
            var result = await _service.ReadAll();

            return ApiOK(result);
        }
    }
}
