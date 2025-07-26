using stc.business.mce.Services.Interfaces;
using stc.dto.mce.Response;
using Core.API.Attributes;
using Core.API.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Response.MAT.QuestionType;
using stc.business.mce.Services;
using stc.dto.mce.Request;
using stc.dto.mce.Request.MAT.QuestionType;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class QuestionTypesController : AppBaseController
    {
        private readonly IQuestionTypeService _service;

        public QuestionTypesController(IQuestionTypeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Thêm loại câu hỏi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(QuestionType_CreateReq request)
        {
            var result = await _service.Create(request, 0);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Lấy danh sách loại câu hỏi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(QuestionType_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _service.List();

            return ApiOK(result);
        }

        /// <summary>
        /// Lấy loại câu hỏi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(QuestionType_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadById(int id)
        {
            var result = await _service.ReadById(id);

            return ApiOK(result);
        }

        /// <summary>
        /// Lấy danh sách cho dropdownlist
        /// </summary>
        /// <returns></returns>
        [HttpGet("dropdownlist")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(QuestionTypes_ReadDropdownList))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> GetDropdownList()
        {
            var result = await _service.GetDropdownList();

            return ApiOK(result);
        }

        /// <summary>
        /// Cập nhật loại câu hỏi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update (QuestionType_UpdateReq request)
        {
            var result = await _service.Update(request, 0);

            return this.ApiOK(result);
        }

        /// <summary>
        /// Xóa loại câu hỏi
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
