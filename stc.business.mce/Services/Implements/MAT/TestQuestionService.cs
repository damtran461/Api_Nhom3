using Core.Common.Configs;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Core.Log;
using Dapper;
using Newtonsoft.Json;
using stc.business.mce.Services.Interfaces.MAT;
using stc.business.mce.Utilities;
using stc.dto.mce.Request;
using stc.dto.mce.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Log.Interface;

namespace stc.business.mce.Services.Implements.MAT
{
    public class TestQuestionService : BaseService, ITestQuestionService
    {
        public TestQuestionService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        {

        }

        /// <summary>
        /// Thêm câu hỏi vào đề thi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        public async Task<CRUDResult<bool>> Create(TestQuestion_CreateReq request, int UpdatedUser)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(Create)}]";

            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdatedUser", UpdatedUser);

                var result = await ReadRepository.Connection.ExecuteAsync("[MAT].[TestQuestion_Create]", parameters, commandType: CommandType.StoredProcedure);

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
        /// Lấy các câu hỏi có trong đề thi bằng id đề thi
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<CRUDResult<IEnumerable<TestQuestion_ReadQuestionByTestRes>>> ReadByTestID(int TestId)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(ReadByTestID)}]";

            try
            {
                var result = await ReadAllQuestionByTest(TestId);

                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<TestQuestion_ReadQuestionByTestRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<TestQuestion_ReadQuestionByTestRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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

                return CRUDError<IEnumerable<TestQuestion_ReadQuestionByTestRes>>(errorMessage: ex.GetSqlExceptionMessage());
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

                return CRUDError<IEnumerable<TestQuestion_ReadQuestionByTestRes>>(errorMessage: ex.GetExceptionMessage());
            }

        }

        /// <summary>
        /// Cập nhật câu hỏi có trong đề thi
        /// </summary>
        /// <param name="request"></param>
        /// <param name="UpdatedUser"></param>
        /// <returns></returns>
        public async Task<CRUDResult<bool>> Update(TestQuestion_UpdateReq request, int UpdatedUser)
        {
            string prefix = $"[{AppCoreConfig.Common.IndexName4Log}][{AppCoreConfig.Common.Environment}][{this.GetType()}][{nameof(Update)}]";


            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdatedUser", UpdatedUser);

                var result = await ReadRepository.Connection.ExecuteAsync("[MAT].[TestQuestion_Update]", parameters, commandType: CommandType.StoredProcedure);

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
        /// Xóa có trong đề thi
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
                    var parameters = new { TestQuestionID = ID, UpdatedUser = UpdatedUser };

                    var result = await ReadRepository.Connection.ExecuteAsync("MAT.TestQuestion_Delete", parameters);

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
        private async Task<IEnumerable<TestQuestion_ReadQuestionByTestRes>> ReadAllQuestionByTest(int ID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAllQuestionByTest)}]";

            try
            {
                string sReadAllProcedure = "MAT.TestQuestion_ReadQuestionByTest";
                var result = await ReadRepository.Connection.QueryAsync<TestQuestion_ReadQuestionByTestRes>(sReadAllProcedure, new { @TestID = ID });
                if (result == null)
                {
                    result = new List<TestQuestion_ReadQuestionByTestRes>();
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
                return await Task.FromResult(new List<TestQuestion_ReadQuestionByTestRes>());
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
                return await Task.FromResult(new List<TestQuestion_ReadQuestionByTestRes>());
            }
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
