using Core.DTO.Response;
using stc.dto.mce.Request;
using stc.dto.mce.Request.MAT.Score;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface IScoresService : IDisposable
    {
        /// <summary>
        /// Tạo Score
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(Score_CreateReq request, int UpdatedUser);

        /// <summary>
        /// Load danh sách Score
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Score_ReadAllRes>>> List();

        /// <summary>
        /// Đọc chi tiết Score theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<Score_ReadAllRes>> ReadByID(int ID);

        /// <summary>
        /// Cập nhật Score theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(Score_UpdateReq request, int UpdatedUser);

        /// <summary>
        /// Xóa Score theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);
    }
}
