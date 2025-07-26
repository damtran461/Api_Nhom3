using stc.business.mce.Services.Interfaces;
using stc.dto.mce.Response;
using Core.API.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Common.TypeFilter;
using stc.business.mce;
using System.Collections.Generic;
using stc.dto.mce.Request;

namespace stc.api.mce.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CourseTypeController : AppBaseController
    {
        private readonly ICourseTypeService _CourseTypeService;

        public CourseTypeController(ICourseTypeService service)
        {
            _CourseTypeService = service;
        }

        /// <summary>
        /// Tạo CourseType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(CourseType_CreateReq request)
        {
            var result = await _CourseTypeService.Create(request, 0);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Load danh sách CourseType
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CourseType_ReadAllRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _CourseTypeService.List();
            return ApiOK(result);
        }

        /// <summary>
        /// Đọc chi tiết CourseType theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(CourseType_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByID(int id)
        {
            var result = await _CourseTypeService.ReadByID(id);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Cập nhật CourseType theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(CourseType_UpdateReq request)
        {
            var updatedResult = await _CourseTypeService.Update(request, 0);
            return this.ApiOK(updatedResult);
        }

        /// <summary>
        /// Xóa CourseType theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _CourseTypeService.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }
    }
}
