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
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Dapper;
using System.Data.SqlClient;

namespace stc.business.mce.Services.Implements
{
    public class BranchService : BaseService, IBranchService
    {
        private readonly Lazy<IRepository> _objRepositoryMSSql;
        private readonly Lazy<IReadOnlyRepository> _objReadOnlyRepositoryMSSql;
        public BranchService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository, 
            IIndex<string, Lazy<IRepository>> objRepositoryMSSql,
            IIndex<string, Lazy<IReadOnlyRepository>> objReadOnlyRepositoryMSSql) : base(logger, repository, readOnlyRepository)
        {
            _objRepositoryMSSql = objRepositoryMSSql[Constants.ConnectionEnum.DefaultMSSql.ToString()];
            _objReadOnlyRepositoryMSSql = objReadOnlyRepositoryMSSql[Constants.ConnectionEnum.DefaultMSSql.ToString()];
        }

        public async Task<CRUDResult<IEnumerable<Branch_ReadAllRes>>> ReadAll()
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(ReadAll)}]";

            try
            {
                var result = await _objReadOnlyRepositoryMSSql.Value.Connection.QueryAsync<Branch_ReadAllRes>("MAT.Branch_ReadAll", commandType: System.Data.CommandType.StoredProcedure);

                return CRUDSuccess(result);
            }
            catch (SqlException ex)
            {
                string message = prefix + $"{Environment.NewLine}{ex.GetSqlExceptionMessage()}";

                _logger.Notify(new LogSendNotify
                {
                    Message = message,
                });

                message += $"{Environment.NewLine}ExceptionObject {JsonConvert.SerializeObject(ex)}";

                _logger.Error(new LogIdentify
                {
                    ProcessID = Guid.NewGuid().ToString()
                }, message);

                return CRUDError<IEnumerable<Branch_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage());
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

                return CRUDError<IEnumerable<Branch_ReadAllRes>>(errorMessage: ex.GetExceptionMessage());
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
