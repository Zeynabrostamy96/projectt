using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using TapLearn.Core.Services.interfaces;

namespace TapLearn.Core.Security
{
    public class PermissionCheckerAttibute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermissionService _permissionService;
        private int _permissionid = 0;
        public PermissionCheckerAttibute(int permissionId)
        {
            _permissionid = permissionId;
        }
        public PermissionCheckerAttibute(IPermissionService permissionChecker)
        {
            _permissionService = permissionChecker;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string name = context.HttpContext.User.Identity.Name;
                if (_permissionService.checkerPermission(_permissionid, name))
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");

            }
        }
    }
}

