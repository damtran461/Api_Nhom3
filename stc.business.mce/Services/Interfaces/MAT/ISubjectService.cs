using Core.DTO.Response;
using stc.dto.mce.Request;
using stc.dto.mce.Request.MAT.Subject;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface ISubjectService : IDisposable
    {
        /// <summary>
        /// Tạo Subject
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(Subject_CreateReq request, int CreatedUserID);

        /// <summary>
        /// Load danh sách Subject
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Subject_ReadAllRes>>> List();

        /// <summary>
        /// Load danh sách Subject
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Subject_DropdowRes>>> Dropdown();
        /// <summary>
        /// Đọc chi tiết Subject theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<Subject_ReadAllRes>> ReadByID(int ID);

        /// <summary>
        /// Cập nhật Subject theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(Subject_UpdateReq request, int UpdatedUser);

        /// <summary>
        /// Xóa Subject theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);

    }
}
