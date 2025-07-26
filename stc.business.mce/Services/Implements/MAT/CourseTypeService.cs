using Core.DataAccess.Interface;
using Core.DTO.Response;
using stc.business.mce.Services.Interfaces;
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
using System.Data;
using System.Linq;

namespace stc.business.mce.Services
{
    public class CourseTypeService : BaseService, ICourseTypeService
    {
        #region Variable 

        #endregion
        #region Constructor

        //Khai báo các đối tượng dùng trong class 
        //logger để thực hiện hoạt động ghi nhật ký (logging)
        public CourseTypeService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        { }

        #endregion
        #region Destructor
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion

        public async Task<CRUDResult<bool>> Create(CourseType_CreateReq request, int UpdatedUser)
        {
            
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Create)}]";
            //string errorMessage = await ColorValidate(request.ColorCode);
            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdatedUser", UpdatedUser);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[CourseType_Create]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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
                var parameters = new { CourseTypeID = ID, UpdatedUser = UpdatedUser };
                var result = await Repository.Connection.ExecuteAsync("[MAT].[CourseType_Delete]", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<CRUDResult<IEnumerable<CourseType_ReadAllRes>>> List()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(List)}]";
            try
            {
                var result = await ReadAll();
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<CourseType_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<CourseType_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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
                return await Task.FromResult(CRUDError<IEnumerable<CourseType_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<IEnumerable<CourseType_ReadAllRes>>(errorMessage: ex.GetExceptionMessage()));
            }
        }

        public async Task<CRUDResult<CourseType_ReadAllRes>> ReadByID(int ID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadByID)}]";

            try
            {
                CourseType_ReadAllRes result;
                result = await ReadRepository.Connection.QueryFirstOrDefaultAsync<CourseType_ReadAllRes>("MAT.CourseType_ReadByID", new { @CourseTypeID = ID });

                if (result == null)
                {
                    return await Task.FromResult(CRUDError<CourseType_ReadAllRes>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<CourseType_ReadAllRes> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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
                return await Task.FromResult(CRUDError<CourseType_ReadAllRes>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<CourseType_ReadAllRes>(errorMessage: ex.GetExceptionMessage()));
            }
        }

        public async Task<CRUDResult<bool>> Update(CourseType_UpdateReq request, int UpdatedUser)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Update)}]";
            //string errorMessageUpdate = await ColorUpdateValidate(request.ColorID);
            
            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdatedUser", UpdatedUser);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[CourseType_Update]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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


        /// <summary>
        /// Load danh sách CourseType từ cache
        /// </summary>
        /// <returns></returns>
        //private IEnumerable<Color_ReadAllRes> ReadAll()
        private async Task<IEnumerable<CourseType_ReadAllRes>> ReadAll()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";

            try
            {
                string sReadAllProcedure = "MAT.CourseType_ReadAll";
                var result = await ReadRepository.Connection.QueryAsync<CourseType_ReadAllRes>(sReadAllProcedure);
                if (result == null)
                {
                    result = new List<CourseType_ReadAllRes>();
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
                return await Task.FromResult(new List<CourseType_ReadAllRes>());
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
                return await Task.FromResult(new List<CourseType_ReadAllRes>());
            }
        }

    }
}
