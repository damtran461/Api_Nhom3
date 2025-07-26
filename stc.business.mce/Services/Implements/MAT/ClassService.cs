using stc.dto.mce.Request;
using stc.dto.mce.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Log.Interface;
using Core.Log;
using Dapper;
using Newtonsoft.Json;
using stc.business.mce.Utilities;
using System.Data.SqlClient;
using System.Linq;
using Core.DTO.Response;
using Core.DataAccess.Interface;
using System.Data;

namespace stc.business.mce.Services
{
    public class ClassService : BaseService, IClassService
    {
        #region Variable 

        #endregion
        #region Constructor

        //Khai báo các đối tượng dùng trong class 
        //logger để thực hiện hoạt động ghi nhật ký (logging)
        public ClassService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        { }

        #endregion
        #region Destructor
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion
        public async Task<CRUDResult<bool>> Create(Class_CreateReq request, int UpdatedUser)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Create)}]";
            //string errorMessage = await ColorValidate(request.ColorCode);
            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("@UpdateUserID", UpdatedUser);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[Class_Create]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<CRUDResult<bool>> Delete(int ID, int UpdatedUser)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Delete)}]";
            try
            {
                var parameters = new { ClassID = ID, UpdateUserID = UpdatedUser };
                var result = await Repository.Connection.ExecuteAsync("MAT.Class_Delete", parameters, commandType: CommandType.StoredProcedure);

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


        public async Task<CRUDResult<IEnumerable<Class_ReadAllRes>>> List()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(List)}]";
            try
            {
                var result = await ReadAll();
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Class_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<Class_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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
                return await Task.FromResult(CRUDError<IEnumerable<Class_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<IEnumerable<Class_ReadAllRes>>(errorMessage: ex.GetExceptionMessage()));
            }
        }

        public async Task<CRUDResult<Class_ReadAllRes>> ReadByID(int ID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadByID)}]";

            try
            {
                Class_ReadAllRes result;
                result = await ReadRepository.Connection.QueryFirstOrDefaultAsync<Class_ReadAllRes>("MAT.Class_ReadByID", new { @ClassID = ID });

                if (result == null)
                {
                    return await Task.FromResult(CRUDError<Class_ReadAllRes>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<Class_ReadAllRes> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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
                return await Task.FromResult(CRUDError<Class_ReadAllRes>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<Class_ReadAllRes>(errorMessage: ex.GetExceptionMessage()));
            }
        }

        public async Task<CRUDResult<bool>> Update(Class_UpdateReq request, int UpdatedUser)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Update)}]";
            //string errorMessageUpdate = await ColorUpdateValidate(request.ColorID);

            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdateUserID", UpdatedUser);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[Class_Update]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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

        private async Task<IEnumerable<Class_ReadAllRes>> ReadAll()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";

            try
            {
                string sReadAllProcedure = "[MAT].[Class_ReadAll]";
                var result = await ReadRepository.Connection.QueryAsync<Class_ReadAllRes>(sReadAllProcedure);
                if (result == null)
                {
                    result = new List<Class_ReadAllRes>();
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
                return await Task.FromResult(new List<Class_ReadAllRes>());
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
                return await Task.FromResult(new List<Class_ReadAllRes>());
            }
        }

        public async Task<CRUDResult<IEnumerable<Class_ReadDropdownList>>> DropdownListClass()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(DropdownListClass)}]";

            try
            {
                string sReadAllProcedure = "[MAT].[Class_Read4DropdownList]";
                var result = await ReadRepository.Connection.QueryAsync<Class_ReadDropdownList>(sReadAllProcedure);
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Class_ReadDropdownList>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<Class_ReadDropdownList>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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
                return await Task.FromResult(CRUDError<IEnumerable<Class_ReadDropdownList>>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<IEnumerable<Class_ReadDropdownList>>(errorMessage: ex.GetExceptionMessage()));
            }
        }


    }
}
