using Cassandra;
using stc.dto.mce.Common;
using Core.Cache;
using Core.DataAccess.Interface;
using Core.DTO.Response;
using Core.Log.Interface;
using System;

namespace stc.business.mce
{
    public abstract class BaseService : IDisposable
    {
        private readonly Lazy<IRepository> _repository;
        protected IRepository Repository => _repository.Value;

        private readonly Lazy<IReadOnlyRepository> _readOnlyRepository;
        protected IReadOnlyRepository ReadRepository => _readOnlyRepository.Value;

        private readonly ICacheManager _cacheManager;
        protected ICacheManager CacheManager => _cacheManager;

        protected readonly ILogger _logger;

        #region Constructor
        public BaseService() { }

        public BaseService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
            _logger = logger;
        }

        public BaseService(ILogger logger, Lazy<IRepository> repository, Lazy<IReadOnlyRepository> readOnlyRepository, ICacheManager cacheManager)
        {
            _logger = logger;
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Function
        protected ApiResult<T> Success<T>(T data)
        {
            var result = new ApiResult<T>()
            {
                data = data,
                code = CRUDStatusCodeRes.Success,
                message = Constants.ErrorCodes[(int)ErrorCodeEnum.Success]
            };

            return result;
        }

        protected ApiResult<T> Success<T>(T data, string message)
        {
            var result = new ApiResult<T>()
            {
                data = data,
                code = CRUDStatusCodeRes.Success,
                message = message
            };

            return result;
        }

        protected CRUDResult<T> CRUDSuccess<T>(T data, string message = null)
        {
            return new CRUDResult<T>()
            {
                Data = data,
                StatusCode = CRUDStatusCodeRes.Success,
                ErrorMessage = message
            };
        }

        protected ApiResult<T> Error<T>(T data = default(T), CRUDStatusCodeRes error_code = CRUDStatusCodeRes.ResourceNotFound, string error_message = "")
        {
            var result = new ApiResult<T>()
            {
                data = data,
                code = error_code,
                message = string.IsNullOrEmpty(error_message) ? Constants.ErrorCodes[(int)ErrorCodeEnum.NotFound] : error_message.Replace("ERROR:", "")
            };

            return result;
        }
        protected CRUDResult<T> CRUDError<T>(T data = default(T), CRUDStatusCodeRes errorCode = CRUDStatusCodeRes.ResetContent, string errorMessage = "")
        {
            return new CRUDResult<T>()
            {
                Data = data,
                StatusCode = errorCode,
                ErrorMessage = string.IsNullOrEmpty(errorMessage) ? Constants.ErrorCodes[(int)ErrorCodeEnum.NotFound] : errorMessage.Replace("ERROR:", "")
            };
        }
        #endregion

        #region Dispose
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_repository != null && _repository.IsValueCreated)
                    {
                        _repository.Value.Dispose();
                    }

                    if (_readOnlyRepository != null && _readOnlyRepository.IsValueCreated)
                    {
                        _readOnlyRepository.Value.Dispose();
                    }
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseService()
        {
            Dispose(false);
        }
        #endregion
    }
}
