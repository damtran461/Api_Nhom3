using Core.DTO.Response;
using stc.dto.mce.Request;
using stc.dto.mce.Request.MAT.Member;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.CourseType;
using stc.dto.mce.Response.MAT.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface IMemberService: IDisposable
    { 

        /// <summary>
        /// Load danh sách Member
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Member_ReadAllRes>>> List();

        /// <summary>
        /// Tạo Member
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUserID"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(Member_CreateReq request, int UpdateUserID);
        /// <summary>
        /// Cập nhật Member theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdateUserID"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(Member_UpdateReq request, int UpdateUserID);
        /// <summary>
        /// Xóa Member theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdateUserID"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdateUserID);
        /// <summary>
        /// Đọc chi tiết Member theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<Member_ReadAllRes>> ReadByID(int ID);
        /// <summary>
        /// Lấy danh sách Member cho dropdown
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Member_DropdownRes>>> DropdownMember();

    }
}
