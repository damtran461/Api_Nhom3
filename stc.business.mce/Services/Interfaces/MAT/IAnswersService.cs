using Core.DTO.Response;
using stc.dto.mce.Request.MAT.Answers;
using stc.dto.mce.Request.MAT.Questions;
using stc.dto.mce.Response.MAT.Answers;
using stc.dto.mce.Response.MAT.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface IAnswersService : IDisposable
    {
        /// <summary>
        /// Cập nhật Answers theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(Answers_UpdateReq request, int UpdatedUser);
        /// <summary>
        /// Tạo Answers
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(Answers_CreateReq request, int UpdatedUser);

        /// <summary>
        /// Load danh sách Answers
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Answers_ReadAllRes>>> List();

        /// <summary>
        /// Load danh sách Answers
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Answers_ReadAllRes>>> Read4DropdownList();

        /// <summary>
        /// Đọc chi tiết Answers theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Answers_ReadAllRes>>> ReadByID(int ID);

        /// <summary>
        /// Xóa Answers theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);
    }
}
