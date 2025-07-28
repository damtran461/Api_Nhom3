using Core.API.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Request.MAT.Answers;
using stc.dto.mce.Response.MAT.Answers;
using stc.dto.mce.Response.MAT.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AnswersController : AppBaseController
    {
        private readonly IAnswersService _AnswersService;

        public AnswersController(IAnswersService service)
        {
            _AnswersService = service;
        }

        /// <summary>
        /// Load danh sách Answers
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Questions_ReadAllRes>))]
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var result = await _AnswersService.List();
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
            var result = await _AnswersService.Read4DropdownList();
            return ApiOK(result);
        }

        /// <summary>
        /// Đọc chi tiết Answers theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Answers_ReadAllRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByID(int id)
        {
            var result = await _AnswersService.ReadByID(id);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Xóa Answers theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _AnswersService.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }

        /// <summary>
        /// Cập nhật Answers theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(Answers_UpdateReq request)
        {
            var updatedResult = await _AnswersService.Update(request, 0);
            return this.ApiOK(updatedResult);
        }

        /// <summary>
        /// Tạo Answers
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(Answers_CreateReq request)
        {
            var result = await _AnswersService.Create(request, 0);
            return this.ApiOK(result);
        }
    }
}
