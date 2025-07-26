using stc.business.mce;
using stc.business.mce.Services.Interfaces;
using stc.business.mce.Utilities;
using stc.dto.mce.Response;
using Core.Common.Configs;
using Core.DataAccess.Extentions;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Core.Log;
using Core.Log.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Implements
{
    public class CountryService : BaseService, ICountryService
    {
        public CountryService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        {

        }

        public async Task<CRUDResult<IEnumerable<CountryRes>>> ReadAll()
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(ReadAll)}]";

            try
            {
                var result = await ReadRepository.Connection.QueryStoredProcPgSql<CountryRes>("fns.country_read_all", null, "p_result");

                return CRUDSuccess(result);
            }
            catch (Exception ex)
            {
                string message = prefix + $"{Environment.NewLine}{ex.GetExceptionMessage()}";

                _logger.Notify(new LogSendNotify
                {
                    Message = message,
                });

                message += $"{Environment.NewLine}ExceptionObject {JsonConvert.SerializeObject(ex)}";

                _logger.Error(new LogIdentify
                {
                    ProcessID = Guid.NewGuid().ToString()
                }, message);

                return CRUDError<IEnumerable<CountryRes>>(errorMessage: ex.GetExceptionMessage());
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
