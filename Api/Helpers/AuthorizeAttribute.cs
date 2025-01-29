using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiRest.Helpers
{
    /// <summary>
    /// Clase validacion de token
    /// </summary>

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {


        /// <summary>
        /// Metodo que valida el token
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.Items["UserId"];
            if (userName is null)
            {
                context.Result = new JsonResult(new { message = "Unautthorized", }) { StatusCode = StatusCodes.Status401Unauthorized };                 
            }
        }
    }
}
