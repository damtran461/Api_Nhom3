using Autofac.Features.Indexed;
using Core.API.Attributes;
using Core.Cache;
using Core.DTO.Response;
using stc.api.mce.Controllers;
using stc.business.mce.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CoC.API.MBM.Controllers
{
    /// <summary>
    /// Utilitie
    /// </summary>
    public class UtilitiesController : AppBaseController
    {
        private readonly ICacheManager _objAPPCacheManager;

        public UtilitiesController()
        {
        }

        ///// <summary>
        ///// RemoveCache
        ///// </summary>
        ///// <param name="CacheKey"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[ResponseType(typeof(bool))]
        //[ApiAuthorize]
        //[Route("api/Utilities/RemoveCache")]
        //public IHttpActionResult RemoveCache(string CacheKey)
        //{
        //    _objAPPCacheManager.RemoveByPattern(CacheKey);
        //    return ApiHelper.ReturnHttpAction(
        //        new CRUDResult<bool> { StatusCode = CRUDStatusCodeRes.Success, Data = true }, this);
        //}
    }
}
