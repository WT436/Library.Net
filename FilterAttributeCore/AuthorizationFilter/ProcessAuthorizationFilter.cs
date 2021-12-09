using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FilterAttributeCore.AuthorizationFilter
{
    /* Uses :
     * 
     **/
    public class ProcessAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _permission = "Read";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = CheckUserPermission(context.HttpContext.User, _permission);
            if (isAuthorized)
            {
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool CheckUserPermission(ClaimsPrincipal user, string permission)
        {
            // Logic for checking the user permission goes here. 

            // Let's assume this user has only read permission.
            return permission == "Read";
        }
    }
}
