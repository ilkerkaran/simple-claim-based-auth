using ClaimBasedAuthorization.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimBasedAuthorization.Filters
{
    public class PermissionBasedAuthorizeAttribute : TypeFilterAttribute
    {
        public PermissionBasedAuthorizeAttribute(params PermissionType[] permissionArray) : base(typeof(PermissionRequirementFilter))
        {
            Arguments = new object[] { permissionArray };
        }
    }
}
