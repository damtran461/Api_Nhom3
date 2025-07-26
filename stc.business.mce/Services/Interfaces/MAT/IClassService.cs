using Core.DTO.Response;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services
{
    public interface IClassService : IDisposable
    {
        /// <summary>
        /// Tạo CourseType
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(Class_CreateReq request, int UpdatedUser);

        /// <summary>
        /// Load danh sách CourseType
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Class_ReadAllRes>>> List();

        /// <summary>
        /// Đọc chi tiết CourseType theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<Class_ReadAllRes>> ReadByID(int ID);

        /// <summary>
        /// Cập nhật CourseType theo ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(Class_UpdateReq request, int UpdatedUser);

        /// <summary>
        /// Xóa CourseType theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);


        /// <summary>
        /// Đọc chi tiết CourseType theo mã ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<Class_ReadDropdownList>>> DropdownListClass();



    }
}
