using Autofac.Features.Indexed;
using Core.Common.Configs;
using Core.DataAccess.Extentions;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Core.Log;
using Core.Log.Interface;
using Dapper;
using Newtonsoft.Json;
using Renci.SshNet.Messages;
using stc.business.mce.Services.Interfaces.MAT;
using stc.business.mce.Utilities;
using stc.dto.mce.Request.MAT.QuestionType;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.QuestionType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Implements.MAT
{
    public class QuestionTypeService: BaseService, IQuestionTypeService
    {
        public QuestionTypeService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        {

        }

        /// <summary>
        /// Thêm câu hỏi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        public async Task<CRUDResult<bool>> Create(QuestionType_CreateReq request, int UpdatedUser)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(Create)}]";

            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdatedUser", UpdatedUser);

                var result = await ReadRepository.Connection.ExecuteAsync("[MAT].[QuestionTypes_Create]", parameters, commandType: CommandType.StoredProcedure);

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
        /// Lấy tất cả danh sách câu hỏi
        /// </summary>
        /// <returns></returns>
        public async Task<CRUDResult<IEnumerable<QuestionType_ReadAllRes>>> List()
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(List)}]";

            try
            {
                var result = await ReadAll();

                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<QuestionType_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<QuestionType_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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

                return CRUDError<IEnumerable<QuestionType_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage());
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

                return CRUDError<IEnumerable<QuestionType_ReadAllRes>>(errorMessage: ex.GetExceptionMessage());
            }
        }

        /// <summary>
        /// Lấy thông tin câu hỏi bằng ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<CRUDResult<QuestionType_ReadAllRes>> ReadById(int ID)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(ReadById)}]";

            try
            {
                QuestionType_ReadAllRes result;
                result = await ReadRepository.Connection.QueryFirstOrDefaultAsync<QuestionType_ReadAllRes>("MAT.QuestionTypes_ReadById", new { @QuestionTypeID = ID });

                if (result == null)
                {
                    return await Task.FromResult(CRUDError<QuestionType_ReadAllRes>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<QuestionType_ReadAllRes> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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

                return CRUDError<QuestionType_ReadAllRes>(errorMessage: ex.GetSqlExceptionMessage());
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

                return CRUDError<QuestionType_ReadAllRes>(errorMessage: ex.GetExceptionMessage());
            }

        }

        public async Task<CRUDResult<IEnumerable<QuestionTypes_ReadDropdownList>>> GetDropdownList()
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(GetDropdownList)}]";

            try
            {
                var result = await ReadRepository.Connection.QueryAsync<QuestionTypes_ReadDropdownList>("[MAT].[QuestionTypes_Read4DropdownList]");
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<QuestionTypes_ReadDropdownList>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<QuestionTypes_ReadDropdownList>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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

                return CRUDError<IEnumerable<QuestionTypes_ReadDropdownList>>(errorMessage: ex.GetSqlExceptionMessage());
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

                return CRUDError<IEnumerable<QuestionTypes_ReadDropdownList>>(errorMessage: ex.GetExceptionMessage());
            }
        }


        public async Task<CRUDResult<bool>> Update(QuestionType_UpdateReq request, int UpdatedUser)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(Update)}]";


            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdatedUser", UpdatedUser);

                var result = await ReadRepository.Connection.ExecuteAsync("[MAT].[QuestionTypes_Update]", parameters, commandType: CommandType.StoredProcedure);

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
        /// Xóa loại câu hỏi
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
                    var parameters = new { QuestionTypeID = ID, UpdatedUser = UpdatedUser };

                    var result = await ReadRepository.Connection.ExecuteAsync("MAT.QuestionTypes_Delete", parameters);

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
        /// Load danh sách QuestionType từ cache
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<QuestionType_ReadAllRes>> ReadAll()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";

            try
            {
                string sReadAllProcedure = "MAT.QuestionTypes_ReadAll";
                var result = await ReadRepository.Connection.QueryAsync<QuestionType_ReadAllRes>(sReadAllProcedure);
                if (result == null)
                {
                    result = new List<QuestionType_ReadAllRes>();
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
                return await Task.FromResult(new List<QuestionType_ReadAllRes>());
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
                return await Task.FromResult(new List<QuestionType_ReadAllRes>());
            }
        }

        

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
