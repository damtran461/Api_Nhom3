using Core.DTO.Response;
using stc.dto.mce.Request.MAT.ClassTest;
using stc.dto.mce.Request.MAT.Member;
using stc.dto.mce.Response.MAT.Class_Test;
using stc.dto.mce.Response.MAT.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface IClassTestService: IDisposable
    {
        /// <summary>
        /// Load danh sách thi
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<ClassTest_ReadAllRes>>> List();
        /// <summary>
        /// Tạo Danh sách thi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUserID"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(ClassTest_CreateReq request, int UpdateUserID);
        /// <summary>
        /// Cập nhật danh sách thi theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdateUserID"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(ClassTest_UpdateReq request, int UpdateUserID);
        /// <summary>
        /// Xóa Danh sách thi theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdateUserID"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdateUserID);
        /// <summary>
        /// Đọc chi tiết danh sách thi theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<ClassTest_ReadAllRes>> ReadByID(int ID);
        /// <summary>
        /// Đọc chi tiết danh sách có member theo idclass
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<ClassTest_ReadMemberInClass>>> ReadMemberByIDClass(int ID);
    }
}
