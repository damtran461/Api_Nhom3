    using stc.business.mce.Services.Interfaces;
using stc.dto.mce.Response;
using Core.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Common.TypeFilter;
using stc.business.mce;
using System.Collections.Generic;
using stc.dto.mce.Request;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Response.MAT.Member;
using Core.DTO.Response;
using stc.business.mce.Services;
using stc.dto.mce.Request.MAT.Member;
using stc.dto.mce.Response.MAT.CourseType;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _MemberService;

        public MemberController(IMemberService service)
        {
            _MemberService = service;
        }

        /// <summary>
        /// Load danh sách Member
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Member_ReadAllRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _MemberService.List();
            return ApiOK(result);
        }
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(Member_CreateReq request)
        {
            var result = await _MemberService.Create(request, 0);
            return ApiOK(result);
        }
        [HttpPut("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(Member_UpdateReq request)
        {
            var updatedResult = await _MemberService.Update(request, 0);
            return this.ApiOK(updatedResult);
        }
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _MemberService.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }

        [HttpGet("GETID/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Member_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByID(int id)
        {
            var result = await _MemberService.ReadByID(id);
            return this.ApiOK(result);
        }
        [HttpGet("dropdown")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Member_DropdownRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Dropdown()
        {
            var result = await _MemberService.DropdownMember();
            return this.ApiOK(result);
        }

        protected IActionResult ApiOK<T>(CRUDResult<T> obj)
        {
            if (obj.StatusCode == CRUDStatusCodeRes.Success)
            {
                return Ok(obj.Data);
            }

            if (obj.StatusCode == CRUDStatusCodeRes.ReturnWithData)
            {
                return Created(string.Empty, obj.Data);
            }

            if (obj.StatusCode == CRUDStatusCodeRes.ResourceNotFound || obj.StatusCode == CRUDStatusCodeRes.Deny)
            {
                return NoContent();
            }

            if (obj.StatusCode == CRUDStatusCodeRes.InvalidData || obj.StatusCode == CRUDStatusCodeRes.ResetContent)
            {
                return StatusCode(406, obj.ErrorMessage);
            }

            return StatusCode(500, obj.ErrorMessage);
        }

    }
}
