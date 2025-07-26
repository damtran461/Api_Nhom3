using Core.API.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stc.business.mce.Services.Interfaces.MAT;
using stc.dto.mce.Request.MAT.Score;
using stc.dto.mce.Response.MAT.Score;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.api.mce.Controllers.MAT
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : AppBaseController
    {
        private readonly IScoresService _ScoreService;

        public ScoresController(IScoresService ScoreService)
        {
            _ScoreService = ScoreService;
        }
        /// <summary>
        /// Tạo Score
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Create(Score_CreateReq request)
        {
            var result = await _ScoreService.Create(request, 0);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Load danh sách Score
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Score_ReadAllRes>))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> List()
        {
            var result = await _ScoreService.List();
            return ApiOK(result);
        }


        /// <summary>
        /// Đọc chi tiết Score theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(Score_ReadAllRes))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> ReadByID(int id)
        {
            var result = await _ScoreService.ReadByID(id);
            return this.ApiOK(result);
        }

        /// <summary>
        /// Cập nhật Score theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Update(Score_UpdateReq request)
        {
            var updatedResult = await _ScoreService.Update(request, 0);
            return this.ApiOK(updatedResult);
        }

        /// <summary>
        /// Xóa Score theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ApiAuthorize(false)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _ScoreService.Delete(id, 0);
            return this.ApiOK(deleteResult);
        }
    }
}
