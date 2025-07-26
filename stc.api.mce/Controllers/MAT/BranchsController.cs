using Core.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services.Interfaces;
using stc.dto.mce.Response;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BranchsController : AppBaseController
    {
        private readonly IBranchService _service;

        public BranchsController(IBranchService service)
        {
            _service = service;
        }

        /// <summary>
        /// ReadAll
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Branch_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadAll()
        {
            var result = await _service.ReadAll();

            return ApiOK(result);
        }
    }
}
