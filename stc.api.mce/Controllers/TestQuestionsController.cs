using Core.API.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class TestQuestionsController : AppBaseController
    {
        private readonly ITestQuestionService _service;

        public TestQuestionsController(ITestQuestionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Thêm câu hỏi vào đề thi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(TestQuestion_CreateReq request)
        {
            var result = await _service.Create(request, 0);
            return this.ApiOK(result);
        }



        /// <summary>
        /// Lấy câu hỏi có trong đề thi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(TestQuestion_ReadQuestionByTestRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByTestId(int id)
        {
            var result = await _service.ReadByTestID(id);

            return ApiOK(result);
        }

        /// <summary>
        /// Cập nhật câu hỏi có trong đề thi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(TestQuestion_UpdateReq request)
        {
            var result = await _service.Update(request, 0);

            return this.ApiOK(result);
        }

        /// <summary>
        /// Xóa câu hỏi có trong đề thi
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
