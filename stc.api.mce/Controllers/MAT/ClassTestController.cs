using Core.API.Attributes;
using Core.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services.Implements.MAT;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Request.MAT.ClassTest;
using stc.dto.mce.Request.MAT.Member;
using stc.dto.mce.Response.MAT.Class_Test;
using stc.dto.mce.Response.MAT.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ClassTestController : ControllerBase
    {
        private readonly IClassTestService _ClassTestService;

        public ClassTestController(IClassTestService service)
        {
            _ClassTestService = service;
        }
        /// <summary>
        /// Load danh sách thi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassTest_ReadAllRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _ClassTestService.List();
            return ApiOK(result);
        }
        /// <summary>
        /// Thêm danh sách thi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(ClassTest_CreateReq request)
        {
            var result = await _ClassTestService.Create(request, 0);
            return ApiOK(result);
        }
        /// <summary>
        /// Cập nhật danh sách thi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(ClassTest_UpdateReq request)
        {
            var updatedResult = await _ClassTestService.Update(request, 0);
            return this.ApiOK(updatedResult);
        }
        /// <summary>
        /// Xóa danh sách thi
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _ClassTestService.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }
        /// <summary>
        /// Tìm kiếm danh sách thi theo ID
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GETID/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(ClassTest_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByID(int id)
        {
            var result = await _ClassTestService.ReadByID(id);
            return this.ApiOK(result);
        }
        /// <summary>
        /// Tìm kiếm danh sách học viên theo ID lớp (ClassID)
        /// </summary>
        /// <param name="id">ID của lớp cần tìm kiếm</param>
        /// <returns>Danh sách học viên trong lớp</returns>
        [HttpGet("GetMember/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassTest_ReadMemberInClass>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ReadByIDClass(int id)
        {
            var result = await _ClassTestService.ReadMemberByIDClass(id);        
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
