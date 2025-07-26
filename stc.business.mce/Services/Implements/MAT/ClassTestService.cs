using Core.DataAccess.Interface;
using Core.DTO.Response;
using Core.Log;
using Core.Log.Interface;
using Dapper;
using Newtonsoft.Json;
using stc.business.mce.Services.Interfaces.MAT;
using stc.business.mce.Utilities;
using stc.dto.mce.Request.MAT.ClassTest;
using stc.dto.mce.Response.MAT.Class_Test;
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
    public class ClassTestService : BaseService, IClassTestService 
    {
        #region Variable 

        #endregion
        #region Constructor

        //Khai báo các đối tượng dùng trong class 
        //logger để thực hiện hoạt động ghi nhật ký (logging)
        public ClassTestService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository) : base(logger, repository, readOnlyRepository)
        { }



        #endregion
        #region Destructor
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion
        public async Task<CRUDResult<IEnumerable<ClassTest_ReadAllRes>>> List()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(List)}]";
            try
            {
                var result = await ReadAll();
                if (result == null || !result.Any())
                {
                    return await Task.FromResult(CRUDError<IEnumerable<ClassTest_ReadAllRes>>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else return await Task.FromResult(new CRUDResult<IEnumerable<ClassTest_ReadAllRes>> { StatusCode = CRUDStatusCodeRes.Success, Data = result });
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
                return await Task.FromResult(CRUDError<IEnumerable<ClassTest_ReadAllRes>>(errorMessage: ex.GetSqlExceptionMessage()));
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
                return await Task.FromResult(CRUDError<IEnumerable<ClassTest_ReadAllRes>>(errorMessage: ex.GetExceptionMessage()));
            }
        }
        private async Task<IEnumerable<ClassTest_ReadAllRes>> ReadAll()
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadAll)}]";

            try
            {
                string sReadAllProcedure = "MAT.ClassTest_ReadAll";
                var result = await ReadRepository.Connection.QueryAsync<ClassTest_ReadAllRes>(sReadAllProcedure);

                if (result == null)
                {
                    result = new List<ClassTest_ReadAllRes>();
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
                return await Task.FromResult(new List<ClassTest_ReadAllRes>());
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
                return await Task.FromResult(new List<ClassTest_ReadAllRes>());
            }
        }

        public async Task<CRUDResult<bool>> Create(ClassTest_CreateReq request, int UpdateUserID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Create)}]";
            
            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdateUserID", UpdateUserID);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[ClassTest_Create]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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

        public async Task<CRUDResult<bool>> Update(ClassTest_UpdateReq request, int UpdateUserID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(Update)}]";
            //string errorMessageUpdate = await ColorUpdateValidate(request.ColorID);

            try
            {
                var parameters = new DynamicParameters(request);
                parameters.Add("UpdateUserID", UpdateUserID);
                var result = await Repository.Connection.ExecuteAsync("[MAT].[ClassTest_Update]", parameters, commandType: System.Data.CommandType.StoredProcedure);

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
                var parameters = new { ClassTestID = ID, UpdateUserID = UpdateUserID };
                var result = await Repository.Connection.ExecuteAsync("[MAT].[ClassTest_Delete]", parameters, commandType: CommandType.StoredProcedure);

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

        public async Task<CRUDResult<ClassTest_ReadAllRes>> ReadByID(int ID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadByID)}]";

            try
            {
                // Khởi tạo kết quả
                var classTestResult = new ClassTest_ReadAllRes();

                // Thực hiện truy vấn và nhận tất cả kết quả từ stored procedure
                var results = await ReadRepository.Connection.QueryAsync<ClassTest_ReadAllRes, MemberInfo, ClassTest_ReadAllRes>(
                    "MAT.ClassTest_ReadByID",
                    (classTest, member) =>
                    {
                        // Kiểm tra nếu classTest đã được khởi tạo
                        if (classTestResult.ClassTestID == 0)
                        {
                            classTestResult.ClassTestID = classTest.ClassTestID;
                            classTestResult.ClassID = classTest.ClassID;
                            classTestResult.ExamID = classTest.ExamID;
                            classTestResult.IsActive = classTest.IsActive;
                            classTestResult.CreateDate = classTest.CreateDate;
                            // Nếu bạn có ClassName và TestName, bạn cũng cần ánh xạ ở đây
                            classTestResult.ClassName = classTest.ClassName; // Cần đảm bảo rằng trường này có trong SELECT
                            classTestResult.TestName = classTest.TestName;   // Cần đảm bảo rằng trường này có trong SELECT
                        }

                        // Nếu member không phải là null, thêm vào danh sách
                        if (member != null)
                        {
                            classTestResult.Members.Add(member);
                        }

                        return classTestResult;
                    },
                    new { @ClassTestID = ID },
                    splitOn: "MemberID" // Chỉ định cột bắt đầu cho việc tách MemberInfo
                );

                // Kiểm tra xem có dữ liệu không
                if (!results.Any())
                {
                    return await Task.FromResult(CRUDError<ClassTest_ReadAllRes>(errorCode: CRUDStatusCodeRes.ResourceNotFound));
                }
                else
                {
                    // Trả về kết quả thành công
                    return await Task.FromResult(new CRUDResult<ClassTest_ReadAllRes>
                    {
                        StatusCode = CRUDStatusCodeRes.Success,
                        Data = classTestResult
                    });
                }
            }
            catch (SqlException ex)
            {
                // Notify and log SQL exceptions
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message });
                message += $"{Environment.NewLine}SqlException {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                return await Task.FromResult(CRUDError<ClassTest_ReadAllRes>(errorMessage: ex.GetSqlExceptionMessage()));
            }
            catch (Exception ex)
            {
                // Notify and log general exceptions
                string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                //_logger.Notify(new LogSendNotify { Message = message });
                message += $"{Environment.NewLine}Exception {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                return await Task.FromResult(CRUDError<ClassTest_ReadAllRes>(errorMessage: ex.GetExceptionMessage()));
            }
        }





        public async Task<CRUDResult<IEnumerable<ClassTest_ReadMemberInClass>>> ReadMemberByIDClass(int ID)
        {
            string prefix = $"{Constants.LogPrefix}[{this.GetType().Name}][{nameof(ReadMemberByIDClass)}]";

            try
            {
                var result = await ReadRepository.Connection.QueryAsync<ClassTest_ReadMemberInClass>(
                    "MAT.GetMembersByClassID",
                    new { @ClassID = ID }
                );
                if (result == null || !result.Any())
                {
                    return CRUDError<IEnumerable<ClassTest_ReadMemberInClass>>(errorCode: CRUDStatusCodeRes.ResourceNotFound);
                }

                // Trả về kết quả thành công với dữ liệu
                return new CRUDResult<IEnumerable<ClassTest_ReadMemberInClass>>
                {
                    StatusCode = CRUDStatusCodeRes.Success,
                    Data = result
                };
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi SQL
                string message = $"{prefix}{Environment.NewLine}{ex.GetSqlExceptionMessage()}";
                message += $"{Environment.NewLine}SqlException {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                // Trả về kết quả lỗi
                return CRUDError<IEnumerable<ClassTest_ReadMemberInClass>>(errorMessage: ex.GetSqlExceptionMessage());
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                string message = $"{prefix}{Environment.NewLine}{ex.GetExceptionMessage()}";
                message += $"{Environment.NewLine}Exception {JsonConvert.SerializeObject(ex)}";
                _logger.Error(new LogIdentify(), message);

                // Trả về kết quả lỗi
                return CRUDError<IEnumerable<ClassTest_ReadMemberInClass>>(errorMessage: ex.GetExceptionMessage());
            }
        }

    }
}
