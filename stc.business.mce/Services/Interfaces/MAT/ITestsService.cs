using Core.DTO.Response;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.QuestionType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface ITestsService: IDisposable
    {
        /// <summary>
        /// Tạo Tests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(Tests_CreateReq request, int UpdatedUser);

        /// <summary>
        /// Load danh sách Tests
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Tests_ReadAllRes>>> List();

        /// <summary>
        /// Đọc chi tiết Tests theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<Tests_ReadAllRes>> ReadByID(int ID);

        /// <summary>
        /// Lấy danh sách đề thi cho dropdownlist
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Tests_ReadDropdownList>>> GetDropdownList();


        /// <summary>
        /// Cập nhật Tests theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(Tests_UpdateReq request, int UpdatedUser);

        /// <summary>
        /// Xóa Tests theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);


    }
}
