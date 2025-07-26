using Core.DTO.Response;
using stc.dto.mce.Request;
using stc.dto.mce.Request.MAT.Questions;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface IQuestionsService : IDisposable
    {
        /// <summary>
        /// Tạo Questions
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(Questions_CreateReq request, int UpdatedUser);

        /// <summary>
        /// Load danh sách Questions
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Questions_ReadAllRes>>> List();

        /// <summary>
        /// Load danh sách Read4DropdownList
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Questions_ReadAllRes>>> Read4DropdownList();

        /// <summary>
        /// Đọc chi tiết Questions theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<Questions_ReadAllRes>> ReadByID(int ID);

        /// <summary>
        /// Cập nhật Questions theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(Question_UpdateReq request, int UpdatedUser);

        /// <summary>
        /// Xóa Questions theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);
    }
}
