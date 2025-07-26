using stc.dto.mce.Response;
using Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces
{
    public interface IBranchService : IDisposable
    {
        Task<CRUDResult<IEnumerable<Branch_ReadAllRes>>> ReadAll();
    }
}
