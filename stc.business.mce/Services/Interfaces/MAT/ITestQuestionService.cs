using Core.DTO.Response;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface ITestQuestionService : IDisposable
    {

        /// <summary>
        /// Tạo TestQuestion
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(TestQuestion_CreateReq request, int UpdatedUser);

        /// <summary>
        /// Đọc chi tiết TestQuestion theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<TestQuestion_ReadQuestionByTestRes>>> ReadByTestID(int TestId);

        /// <summary>
        /// Cập nhật TestQuestion theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(TestQuestion_UpdateReq request, int UpdatedUser);

        /// <summary>
        /// Xóa TestQuestion theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);


    }
}
