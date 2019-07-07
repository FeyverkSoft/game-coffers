using Coffers.Public.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Authorization
{
    public class PermissionRequiredAttribute : TypeFilterAttribute
    {
        public PermissionRequiredAttribute(params string[] roles) : base(typeof(PermissionAuthorizationFilter))
        {
            Arguments = new object[] { roles };
        }
    }
}
