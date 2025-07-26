using stc.dto.mce.Response;
using Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Interfaces
{
    public interface ICountryService : IDisposable
    {
        Task<CRUDResult<IEnumerable<CountryRes>>> ReadAll();
    }
}
