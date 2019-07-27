using System;
using Coffers.Public.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Authorization
{
    public class PermissionRequiredAttribute : TypeFilterAttribute
    {
        public PermissionRequiredAttribute(params String[] roles) : base(typeof(PermissionAuthorizationFilter))
        {
            Arguments = new Object[] { roles };
        }
    }
}
