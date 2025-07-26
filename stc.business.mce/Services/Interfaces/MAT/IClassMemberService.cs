using Core.DTO.Response;
using stc.dto.mce.Common;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.ClassMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface IClassMemberService : IDisposable
    {

        /// <summary>
        /// Hiển thị học viên theo ID lớp
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<ClassMember_ReadAllRes>>> ReadByClassID(int ID);

        /// <summary>
		/// Thêm học viên vào lớp
		/// </summary>
		/// <param name="request"></param>
		/// <param name="UpdatedUser"></param>
		/// <returns></returns>
		Task<CRUDResult<bool>> Create(ClassMember_CreateReq request, int UpdatedUser);

        /// <summary>
        /// Xóa học viên khỏi lớp
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);
    }
}
