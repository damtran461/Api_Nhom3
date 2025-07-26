using Core.DataAccess.Interface;
using Core.DTO.Response;
using Core.Log;
using Core.Log.Interface;
using Dapper;
using Newtonsoft.Json;
using stc.business.mce.Services.Interfaces.MAT;
using stc.business.mce.Utilities;
using stc.dto.mce.Request.MAT.Answers;
using stc.dto.mce.Request.MAT.Questions;
using stc.dto.mce.Response.MAT.Answers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Implements.MAT
{
    public class AnswersService : BaseService, IAnswersService
    {
        #region Variable 

        #endregion
        #region Constructor

        //Khai báo các đối tượng dùng trong class 
        //logger để thực hiện hoạt động ghi nhật ký (logging)
        public AnswersService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        { }

        #endregion
        #region Destructor
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        private async Task<IEnumerable<Answers_ReadAllRes>> ReadAll()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";

            try
            {
                string sReadAllProcedure = "MAT.Answers_ReadAll";
                var result = await ReadRepository.Connection.QueryAsync<Answers_ReadAllRes>(sReadAllProcedure);
                if (result == null)
                {
                    result = new List<Answers_ReadAllRes>();
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
                return await Task.FromResult(new List<Answers_ReadAllRes>());
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
                return await Task.FromResult(new List<Answers_ReadAllRes>());
            }
        }
         private async Task<IEnumerable<Answers_ReadAllRes>> Dropdown()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";

            try
            {
                string sReadAllProcedure = "MAT.Answer_Read4DropdownList";
                var result = await ReadRepository.Connection.QueryAsync<Answers_ReadAllRes>(sReadAllProcedure);
                if (result == null)
                {
                    result = new List<Answers_ReadAllRes>();
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
                return await Task.FromResult(new List<Answers_ReadAllRes>());
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
                return await Task.FromResult(new List<Answers_ReadAllRes>());
            }
        }


        public async Task<CRUDResult<IEnumerable<Answers_ReadAllRes>>> List()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(List)}]";
            try
            {
                var result = await ReadAll();
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<Answers_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
            }
            catch (SqlException ex)
            {
                //notify
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message }); { Message = message });

                //log error mesage
                message += $"{Environment.NewLine}SqlException {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                //return result
                return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorMessage: ex.GetExceptionMessage()));
            }
        }
        public async Task<CRUDResult<IEnumerable<Answers_ReadAllRes>>> Read4DropdownList()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Read4DropdownList)}]";
            try
            {
                var result = await Dropdown();
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<Answers_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
            }
            catch (SqlException ex)
            {
                //notify
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message }); { Message = message });

                //log error mesage
                message += $"{Environment.NewLine}SqlException {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                //return result
                return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorMessage: ex.GetExceptionMessage()));
            }
        }

        public async Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Delete)}]";
            try
            {
                var parameters = new { AnswerID = ID, UpdateUserID = UpdatedUser };
                var result = await Repository.Connection.ExecuteAsync("[MAT].[Anwsers_Delete]", parameters, commandType: CommandType.StoredProcedure);

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


        public async Task<CRUDResult<IEnumerable<Answers_ReadAllRes>>> ReadByID(int ID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadByID)}]";

            try
            {
                IEnumerable<Answers_ReadAllRes> result;
                result = await ReadRepository.Connection.QueryAsync<Answers_ReadAllRes>("MAT.Answers_ReadByID", new { @QuestionID = ID });

                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else
                {
                    return await Task.FromResult(new CRUDResult<IEnumerable<Answers_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
                }
            }
            catch (SqlException ex)
            {
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                _logger.Error(new LogIdentify(), message);
                return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage()));
            }
            catch (Exception ex)
            {
                string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                _logger.Error(new LogIdentify(), message);
                return await Task.FromResult(CRUDError<IEnumerable<Answers_ReadAllRes>>(errorMessage: ex.GetExceptionMessage()));
            }
        }


        public async Task<CRUDResult<bool>> Create(Answers_CreateReq request, int UpdatedUser)
        {

            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Create)}]";
            //string errorMessage = await ColorValidate(request.ColorCode);
            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdateUserID", UpdatedUser);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[Anwsers_Create]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<CRUDResult<bool>> Update(Answers_UpdateReq request, int UpdatedUser)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Update)}]";
            //string errorMessageUpdate = await ColorUpdateValidate(request.ColorID);

            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdateUserID", UpdatedUser);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[Anwsers_Update]", parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (result == 1)
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
}
