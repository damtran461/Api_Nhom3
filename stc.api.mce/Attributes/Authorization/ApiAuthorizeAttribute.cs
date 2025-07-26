using stc.business.mce;
using stc.business.mce.Helpers;
using stc.dto.mce.Common;
using Core.Common;
using Core.Common.Model;
using Core.Log;
using Core.Log.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace stc.api.mce.Configs
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private bool _isAuthorizeAction { get; set; }

        public ApiAuthorizeAttribute(bool AuthorizeAction = false)
        {
            _isAuthorizeAction = AuthorizeAction;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            try
            {
                var logIdentity = new LogIdentify { SessionID = Guid.NewGuid().ToString() };
                var logger = Engine.ContainerManager.Resolve<ILogger>();

                if (filterContext == null)
                {
                    logger.Error(logIdentity, "Empty request");
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }

                var token = filterContext.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    logger.Error(logIdentity, "Request doesn't contain token");
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }

                var claim = AuthenticateHelper.ValidateToken(token);

                if (claim.Count == 0)
                {
                    logger.Error(logIdentity, $"Request has invalid token:\r\n" +
                        $"Token: {token}");
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }

                var userPrincipal = new AppUserPrincipal(claim);
                filterContext.HttpContext.User = userPrincipal;

                if (_isAuthorizeAction) return;

                if (userPrincipal.customer_id <= 0)
                {
                    logger.Error(logIdentity, $"Request doesn't have access token");
                    filterContext.Result = new ForbidResult();
                    return;
                }

                if (ApiConfig.Common.DisableAuthen)
                {
                    return;
                }

                var obj = (HttpMethodActionConstraint)filterContext.ActionDescriptor.ActionConstraints.FirstOrDefault();
                string method = obj.HttpMethods.FirstOrDefault();
                string actionName = (filterContext.ActionDescriptor.RouteValues["action"] == null) ? "" : filterContext.ActionDescriptor.RouteValues["action"];
                string controllerName = (filterContext.ActionDescriptor.RouteValues["controller"] == null) ? "" : filterContext.ActionDescriptor.RouteValues["controller"];
            }
            catch (Exception ex)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }
        }

        public bool IsAuthorize(string controller, string action, string method, List<int> RoleIDs)
        {
            if (RoleIDs == null) return false;

            controller = controller.ToLower();
            action = action.ToLower();
            method = method.ToLower();
            var roleFunctionResult = from r in AppPermission.Data
                                     where RoleIDs.Contains(r.RoleID) && r.APIAction.ToLower() == action && r.APIController.ToLower() == controller && r.APIMethod.ToLower() == method
                                     select r;
            return roleFunctionResult.Any();
        }
    }
}
