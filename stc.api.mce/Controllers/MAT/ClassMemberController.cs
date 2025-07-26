using Core.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using Spire.Pdf.Exporting.XPS.Schema;
using stc.business.mce.Services;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.ClassMember;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ClassMemberController : AppBaseController
    {
        private readonly IClassMemberService _ClassMemberService;

        public ClassMemberController(IClassMemberService service)
        {
            _ClassMemberService = service;
        }

        /// <summary>
        /// Load danh sách học viên trong lớp
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("ReadByClassID/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(ClassMember_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByClassID(int id)
        {
            var result = await _ClassMemberService.ReadByClassID(id);
            return ApiOK(result);
        }

        /// <summary>
        /// Thêm học viên vào lớp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(ClassMember_CreateReq request)
        {
            var result = await _ClassMemberService.Create(request, 0);
            return this.ApiOK(result);
        }

        /// <summary>
        /// xóa học viên khỏi lớp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _ClassMemberService.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }


    }
}
