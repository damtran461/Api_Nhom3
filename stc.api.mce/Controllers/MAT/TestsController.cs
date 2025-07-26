using Core.API.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class TestsController : AppBaseController
    {
        private readonly ITestsService _service;

        public TestsController(ITestsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Thêm đề thi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(Tests_CreateReq request)
        {
            var result = await _service.Create(request, 0);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Lấy danh sách đề thi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Tests_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _service.List();

            return ApiOK(result);
        }

        /// <summary>
        /// Lấy đề thi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Tests_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadById(int id)
        {
            var result = await _service.ReadByID(id);

            return ApiOK(result);
        }

        /// <summary>
        /// Lấy danh sách cho dropdownlist
        /// </summary>
        /// <returns></returns>
        [HttpGet("dropdownlist")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Tests_ReadDropdownList))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> GetDropdownList()
        {
            var result = await _service.GetDropdownList();

            return ApiOK(result);
        }

        /// <summary>
        /// Cập nhật đề thi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(Tests_UpdateReq request)
        {
            var result = await _service.Update(request, 0);

            return this.ApiOK(result);
        }

        /// <summary>
        /// Xóa đề thi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _service.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }

    }
}
