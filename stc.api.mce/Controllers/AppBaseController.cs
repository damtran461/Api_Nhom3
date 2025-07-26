using stc.business.mce;
using stc.dto.mce.Common;
using Core.API.Controller;
using Core.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace stc.api.mce.Controllers
{
    [AllowAnonymous]
    public class AppBaseController : BaseController
    {
        protected AppUserPrincipal AppCurrentUser
        {
            get { return User as AppUserPrincipal; }
        }

        protected IActionResult ApiOK<T>(ApiResult<T> obj)
        {
            switch (obj.code)
            {
                case CRUDStatusCodeRes.Success: // 200
                    {
                        if (obj.message != Constants.ErrorCodes[(int)ErrorCodeEnum.Success])
                        {
                            return this.Accepted(obj);
                        }

                        return this.Ok(obj);
                    }
                case CRUDStatusCodeRes.ResourceNotFound: // 204
                    {
                        return this.StatusCode(statusCode: 204, obj);
                    }
                case CRUDStatusCodeRes.InvalidData: // 406
                    {
                        return this.StatusCode(statusCode: 406, obj);
                    }
                default: // Custom error
                    {
                        return this.StatusCode(statusCode: 406, obj);
                    }
            }
        }
    }
}
