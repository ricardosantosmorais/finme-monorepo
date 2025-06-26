using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Finme.Api.WebApp.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<CustomAuthorizeAttribute>)) as ILogger<CustomAuthorizeAttribute>;
            logger?.LogDebug("CustomAuthorize: Verificando autorização");

            // Verificar se o usuário está autenticado (pelo middleware)
            var user = context.HttpContext.Items["User"] as string;

            if (user == null)
            {
                logger?.LogWarning("CustomAuthorize: Usuário não autenticado");
                context.Result = new JsonResult(new { message = "Não autorizado" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }

            // Se roles específicos são necessários
            if (!string.IsNullOrEmpty(Roles))
            {
                var userRoles = context.HttpContext.Items["UserRoles"] as List<string>;
                var requiredRoles = Roles.Split(',').Select(r => r.Trim()).ToList();

                // Verificar se o usuário tem pelo menos um dos papéis exigidos
                bool hasRole = userRoles != null && requiredRoles.Any(role => userRoles.Contains(role));

                if (!hasRole)
                {
                    logger?.LogWarning($"CustomAuthorize: Usuário '{user}' não tem os papéis necessários");
                    context.Result = new JsonResult(new { message = "Acesso negado" })
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                    return;
                }

                logger?.LogDebug($"CustomAuthorize: Usuário '{user}' autorizado com papéis necessários");
            }
            else
            {
                logger?.LogDebug($"CustomAuthorize: Usuário '{user}' autenticado sem verificação de papéis");
            }
        }
    }
}
