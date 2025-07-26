using Core.API.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services;
using stc.business.mce.Services.Implements.MAT;
using stc.business.mce.Services.Interfaces;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Request;
using stc.dto.mce.Request.MAT.Questions;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class QuestionsController : AppBaseController
    {
        private readonly IQuestionsService _QuestionsSevice;

        public QuestionsController(IQuestionsService service)
        {
            _QuestionsSevice = service;
        }

        /// <summary>
        /// Load danh sách Question
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Questions_ReadAllRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _QuestionsSevice.List();
            return ApiOK(result);
        }


        /// <summary>
        /// Load danh sách Read4DropdownList
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("Read4DropdownList")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Questions_ReadAllRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Read4DropdownList()
        {
            var result = await _QuestionsSevice.Read4DropdownList();
            return ApiOK(result);
        }

        /// <summary>
        /// Tạo Question
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(Questions_CreateReq request)
        {
            var result = await _QuestionsSevice.Create(request, 0);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Đọc chi tiết Question theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Questions_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByID(int id)
        {
            var result = await _QuestionsSevice.ReadByID(id);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Cập nhật Question theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(Question_UpdateReq request)
        {
            var updatedResult = await _QuestionsSevice.Update(request, 0);
            return this.ApiOK(updatedResult);
        }

        /// <summary>
        /// Xóa Question theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _QuestionsSevice.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }
    }
}
