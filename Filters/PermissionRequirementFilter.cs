using ClaimBasedAuthorization.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimBasedAuthorization.Filters
{
    public class PermissionRequirementFilter : IAuthorizationFilter
    {

        private readonly List<PermissionType> allowedPermissionList;

        public AuthorityRequirementFilter(IEnumerable<PermissionType> allowedPermissionList)
        {
            this.allowedPermissionList = allowedPermissionList.ToList();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (false == context.HttpContext.User.Identity.IsAuthenticated)
                context.Result = new UnauthorizedResult();
            //empty allowedPermissionList means we dont need to check authorities
            if (allowedPermissionList.Count > 0
                && false == allowedPermissionList.Any(permission => context.HttpContext.User.IsInRole(((int)permission).ToString())))
                context.Result = new ForbidResult();
        }
    }
}
