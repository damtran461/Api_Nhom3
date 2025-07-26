using Core.DataAccess.Interface;
using Core.DTO.Response;
using Core.Log;
using Core.Log.Interface;
using Dapper;
using Newtonsoft.Json;
using stc.business.mce.Services.Interfaces.MAT;
using stc.business.mce.Utilities;
using stc.dto.mce.Request;
using stc.dto.mce.Request.MAT.Member;
using stc.dto.mce.Response;
using stc.dto.mce.Response.MAT.CourseType;
using stc.dto.mce.Response.MAT.Member;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stc.business.mce.Services.Implements.MAT
{
    public class MemberService : BaseService, IMemberService
    {
        #region Variable 

        #endregion
        #region Constructor

        //Khai báo các đối tượng dùng trong class 
        //logger để thực hiện hoạt động ghi nhật ký (logging)
        public MemberService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        { }

      

        #endregion
        #region Destructor
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion
        public async Task<CRUDResult<IEnumerable<Member_ReadAllRes>>> List()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(List)}]";
            try
            {
                var result = await ReadAll();
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Member_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<Member_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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
                return await Task.FromResult(CRUDError<IEnumerable<Member_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<IEnumerable<Member_ReadAllRes>>(errorMessage: ex.GetExceptionMessage()));
            }
        }
        private async Task<IEnumerable<Member_ReadAllRes>> ReadAll()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";

            try
            {
                string sReadAllProcedure = "MAT.Member_ReadAll";
                var result = await ReadRepository.Connection.QueryAsync<Member_ReadAllRes>(sReadAllProcedure);

                if (result == null)
                {
                    result = new List<Member_ReadAllRes>();
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
                return await Task.FromResult(new List<Member_ReadAllRes>());
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
                return await Task.FromResult(new List<Member_ReadAllRes>());
            }
        }
        public async Task<CRUDResult<bool>> Create(Member_CreateReq request, int UpdateUserID)
        {

            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Create)}]";
            //string errorMessage = await ColorValidate(request.ColorCode);
            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdateUserID", UpdateUserID);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[Member_Create]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<CRUDResult<bool>> Update(Member_UpdateReq request, int UpdateUserID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Update)}]";
            //string errorMessageUpdate = await ColorUpdateValidate(request.ColorID);

            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdateUserID", UpdateUserID);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[Member_Update]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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
        public async Task<CRUDResult<bool>> Delete(int ID, int UpdateUserID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Delete)}]";
            try
            {
                var parameters = new { MemberID = ID, UpdateUserID = UpdateUserID };
                var result = await Repository.Connection.ExecuteAsync("[MAT].[Member_Delete]", parameters, commandType: CommandType.StoredProcedure);

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
        public async Task<CRUDResult<Member_ReadAllRes>> ReadByID(int ID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadByID)}]";

            try
            {
                Member_ReadAllRes result;
                result = await ReadRepository.Connection.QueryFirstOrDefaultAsync<Member_ReadAllRes>("MAT.Member_ReadByID", new { @MemberID = ID });

                if (result == null)
                {
                    return await Task.FromResult(CRUDError<Member_ReadAllRes>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<Member_ReadAllRes> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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
                return await Task.FromResult(CRUDError<Member_ReadAllRes>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<Member_ReadAllRes>(errorMessage: ex.GetExceptionMessage()));
            }
        }


        private async Task<IEnumerable<Member_DropdownRes>> DropdownList()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";
            try
            {
                string sReadAllProcedure = "MAT.Member_Read4DropdownList";
                var result = await ReadRepository.Connection.QueryAsync<Member_DropdownRes>(sReadAllProcedure);
                if (result == null)
                {
                    result = new List<Member_DropdownRes>();
                }
                return result;

            }
            catch (SqlException ex)
            {
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                message += $"{Environment.NewLine}ExceptionObject {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify
                {
                    ProcessID = Guid.NewGuid().ToString()
                }, message);
                return await Task.FromResult(new List<Member_DropdownRes>());
            }
            catch (Exception ex) {

                string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                message += $"{Environment.NewLine}ExceptionObject {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify
                {
                    ProcessID = Guid.NewGuid().ToString()
                }, message);
                return await Task.FromResult(new List<Member_DropdownRes>());

            }

        }

        

         public async Task<CRUDResult<IEnumerable<Member_DropdownRes>>> DropdownMember()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(DropdownMember)}]";
            try
            {
                var result = await DropdownList();
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<Member_DropdownRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<Member_DropdownRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });



            }
            catch (SqlException ex)
            {
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                message += $"{Environment.NewLine}SqlException {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);
                return await Task.FromResult(CRUDError<IEnumerable<Member_DropdownRes>>(errorMessage: ex.GetSqlExceptionMessage()));
            }
            catch (Exception ex)
            {

                string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                message += $"{Environment.NewLine}Exception {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);
                return await Task.FromResult(CRUDError<IEnumerable<Member_DropdownRes>>(errorMessage: ex.GetExceptionMessage()));
            }
        }
    }
}
