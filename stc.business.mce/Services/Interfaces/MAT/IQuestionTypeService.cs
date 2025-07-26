using Autofac.Util;
using Core.DTO.Response;
using stc.dto.mce.Request.MAT.QuestionType;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.QuestionType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces.MAT
{
    public interface IQuestionTypeService : IDisposable
    {
        /// <summary>
        /// Tạo QuestionType
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Create(QuestionType_CreateReq request, int UpdatedUser);


        /// <summary>
        /// Cập nhật QuestionType
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdateUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Update(QuestionType_UpdateReq  request, int UpdatedUser);


        /// <summary>
        /// Danh sách loại câu hỏi
        /// </summary>
        /// <returns></returns>
        Task<CRUDResult<IEnumerable<QuestionType_ReadAllRes>>> List();

        /// <summary>
        /// Lấy loại câu hỏi hỏi theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<CRUDResult<QuestionType_ReadAllRes>> ReadById(int ID);

        Task<CRUDResult<IEnumerable<QuestionTypes_ReadDropdownList>>> GetDropdownList();

        /// <summary>
        /// Xóa loại câu hỏi theo ID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser);
        
    }
}
