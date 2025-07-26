using Core.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services;
using stc.business.mce.Services.Interfaces;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ClassController : AppBaseController
    {
        private readonly IClassService _classService;

        public ClassController(IClassService service)
        {
            _classService = service;
        }

        /// <summary>
        /// Tạo Class
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(Class_CreateReq request)
        {
            var result = await _classService.Create(request, 0);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Load danh sách Class
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Class_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _classService.List();
            return ApiOK(result);
        }

        /// <summary>
        /// Đọc chi tiết Class theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Class_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByID(int id)
        {
            var result = await _classService.ReadByID(id);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Xóa Class theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _classService.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }

        /// <summary>
        /// Cập nhật Class theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(Class_UpdateReq request)
        {
            var updatedResult = await _classService.Update(request, 0);
            return this.ApiOK(updatedResult);
        }

        /// <summary>
        /// Load danh sách Class
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("dropdown")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Class_ReadDropdownList))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> DropdownListClass()
        {
            var result = await _classService.DropdownListClass();
            return ApiOK(result);
        }
    }
}
