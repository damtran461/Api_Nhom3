using Core.Common.Configs;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Core.Log;
using Dapper;
using Newtonsoft.Json;
using stc.business.mce.Services.Interfaces.MAT;
using stc.business.mce.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Log.Interface;
using stc.dto.mce.Request;
using stc.dto.mce.Response;

namespace stc.business.mce.Services.MAT
{
    public class TestsService : BaseService, ITestsService
    {
        public TestsService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        {

        }

        /// <summary>
        /// Thêm đề thi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        public async Task<CRUDResult<bool>> Create(Tests_CreateReq request, int UpdatedUser)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(Create)}]";

            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdatedUser", UpdatedUser);

                var result = await ReadRepository.Connection.ExecuteAsync("[MAT].[Tests_Create]", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    return CRUDSuccess(true);
                }
                else
                {
                    return CRUDError<bool>(errorMessage: Constants.NotUpdatedMessage);
                }
            }
            catch (SqlException ex)
            {
                //notify
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message });

                //log error mesage
                message += $"{Environment.NewLine}SqlException {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                //return result
                return await Task.FromResult(CRUDError<bool>(errorMessage: ex.GetSqlExceptionMessage()));
            }
            catch (Exception ex)
            {
                //notify
                string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message });

                //log error mesage
                message += $"{Environment.NewLine}Exception {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                //return result
                return await Task.FromResult(CRUDError<bool>(errorMessage: ex.GetExceptionMessage()));
            }
        }

        /// <summary>
        /// Lấy tất cả danh sách đề thi
        /// </summary>
        /// <returns></returns>
        public async Task<CRUDResult<IEnumerable<Tests_ReadAllRes>>> List()
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(List)}]";

            try
            {
                var result = await ReadAll();

                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Tests_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<Tests_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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

                return CRUDError<IEnumerable<Tests_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage());
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

                return CRUDError<IEnumerable<Tests_ReadAllRes>>(errorMessage: ex.GetExceptionMessage());
            }
        }

        /// <summary>
        /// Lấy thông tin đề thi bằng ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<CRUDResult<Tests_ReadAllRes>> ReadByID(int ID)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(ReadByID)}]";

            try
            {
                Tests_ReadAllRes result;
                result = await ReadRepository.Connection.QueryFirstOrDefaultAsync<Tests_ReadAllRes>("MAT.Tests_ReadById", new { @TestID = ID });

                if (result == null)
                {
                    return await Task.FromResult(CRUDError<Tests_ReadAllRes>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<Tests_ReadAllRes> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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

                return CRUDError<Tests_ReadAllRes>(errorMessage: ex.GetSqlExceptionMessage());
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

                return CRUDError<Tests_ReadAllRes>(errorMessage: ex.GetExceptionMessage());
            }

        }

        public async Task<CRUDResult<IEnumerable<Tests_ReadDropdownList>>> GetDropdownList()
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(GetDropdownList)}]";

            try
            {
                var result = await ReadRepository.Connection.QueryAsync<Tests_ReadDropdownList>("[MAT].[Tests_Read4DropdownList]");
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Tests_ReadDropdownList>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<Tests_ReadDropdownList>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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

                return CRUDError<IEnumerable<Tests_ReadDropdownList>>(errorMessage: ex.GetSqlExceptionMessage());
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

                return CRUDError<IEnumerable<Tests_ReadDropdownList>>(errorMessage: ex.GetExceptionMessage());
            }
        }


        public async Task<CRUDResult<bool>> Update(Tests_UpdateReq request, int UpdatedUser)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(Update)}]";


            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdatedUser", UpdatedUser);

                var result = await ReadRepository.Connection.ExecuteAsync("[MAT].[Tests_Update]", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    return CRUDSuccess(true);
                }
                else
                {
                    return CRUDError<bool>(errorMessage: Constants.NotUpdatedMessage);
                }
            }
            catch (SqlException ex)
            {
                //notify
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message });

                //log error mesage
                message += $"{Environment.NewLine}SqlException {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                //return result
                return await Task.FromResult(CRUDError<bool>(errorMessage: ex.GetSqlExceptionMessage()));
            }
            catch (Exception ex)
            {
                //notify
                string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message });

                //log error mesage
                message += $"{Environment.NewLine}Exception {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                //return result
                return await Task.FromResult(CRUDError<bool>(errorMessage: ex.GetExceptionMessage()));
            }
        }

        /// <summary>
        /// Xóa loại đề thi
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        public async Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser)
        {

            {
                string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(Delete)}]";

                try
                {
                    var parameters = new { TestID = ID, UpdatedUser = UpdatedUser };

                    var result = await ReadRepository.Connection.ExecuteAsync("MAT.Tests_Delete", parameters);

                    if (result > 0)
                    {
                        return CRUDSuccess(true);
                    }
                    else
                    {
                        return CRUDError<bool>(errorMessage: Constants.NotUpdatedMessage);
                    }
                }
                catch (SqlException ex)
                {
                    //notify
                    string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                    //_logger.Notify(new LogSendNotify { Message = message });

                    //log error mesage
                    message += $"{Environment.NewLine}SqlException {JsonConvert.SerializeObject(ex)}";
                    _logger.Error(new LogIdentify(), message);

                    //return result
                    return await Task.FromResult(CRUDError<bool>(errorMessage: ex.GetSqlExceptionMessage()));
                }
                catch (Exception ex)
                {
                    //notify
                    string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                    //_logger.Notify(new LogSendNotify { Message = message });

                    //log error mesage
                    message += $"{Environment.NewLine}Exception {JsonConvert.SerializeObject(ex)}";
                    _logger.Error(new LogIdentify(), message);

                    //return result
                    return await Task.FromResult(CRUDError<bool>(errorMessage: ex.GetExceptionMessage()));
                }

            }

        }

        /// <summary>
        /// Load danh sách Tests từ cache
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<Tests_ReadAllRes>> ReadAll()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";

            try
            {
                string sReadAllProcedure = "MAT.Tests_ReadAll";
                var result = await ReadRepository.Connection.QueryAsync<Tests_ReadAllRes>(sReadAllProcedure);
                if (result == null)
                {
                    result = new List<Tests_ReadAllRes>();
                }
                return result;
            }
            catch (SqlException ex)
            {
                //notify
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message }); { Message = message });

                //log error mesage
                message += $"{Environment.NewLine}ExceptionObject {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify
                {
                    ProcessID = Guid.NewGuid().ToString()
                }, message);

                //return result
                return await Task.FromResult(new List<Tests_ReadAllRes>());
            }
            catch (Exception ex)
            {
                //notify
                string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message }); { Message = message });

                //log error mesage
                message += $"{Environment.NewLine}ExceptionObject {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify
                {
                    ProcessID = Guid.NewGuid().ToString()
                }, message);

                //return result
                return await Task.FromResult(new List<Tests_ReadAllRes>());
            }
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
