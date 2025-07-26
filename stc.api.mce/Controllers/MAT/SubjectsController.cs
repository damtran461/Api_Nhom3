using Core.API.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services;
using stc.business.mce.Services.Interfaces;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Request;
using stc.dto.mce.Request.MAT.Subject;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.Subject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SubjectsController : AppBaseController
    {
        private readonly ISubjectService _SubjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _SubjectService = subjectService;
        }

        /// <summary>
        /// Tạo Subject
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(Subject_CreateReq request)
        {
            var result = await _SubjectService.Create(request, 0);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Load danh sách Subject
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject_ReadAllRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _SubjectService.List();
            return ApiOK(result);
        }


        /// <summary>
        /// Đọc chi tiết Subject theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Subject_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByID(int id)
        {
            var result = await _SubjectService.ReadByID(id);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Cập nhật Subject theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(Subject_UpdateReq request)
        {
            var updatedResult = await _SubjectService.Update(request, 0);
            return this.ApiOK(updatedResult);
        }

        /// <summary>
        /// Xóa Subject theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _SubjectService.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }


        [HttpGet("dropdown")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject_DropdowRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Dropdown()
        {
            var result = await _SubjectService.Dropdown();
            return ApiOK(result);
        }
    }
}
