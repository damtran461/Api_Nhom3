using Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using stc.dto.mce.Response;
using stc.dto.mce.Request;

namespace stc.business.mce.Services.Interfaces
{
    public interface ICourseTypeService : IDisposable 
    {
        /// <summary>
        /// Tạo CourseType
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(CourseType_CreateReq request, int UpdatedUser);

        /// <summary>
        /// Load danh sách CourseType
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<CourseType_ReadAllRes>>> List();

        /// <summary>
        /// Đọc chi tiết CourseType theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<CourseType_ReadAllRes>> ReadByID(int ID);

        /// <summary>
        /// Cập nhật CourseType theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(CourseType_UpdateReq request, int UpdatedUser);

        /// <summary>
        /// Xóa CourseType theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);


    }
}
