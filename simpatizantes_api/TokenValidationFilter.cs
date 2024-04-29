using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using simpatizantes_api.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace simpatizantes_api.Filters
{
    public class TokenValidationFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dbContext = context.HttpContext.RequestServices.GetService<ApplicationDbContext>();

            // Obtener el token de la solicitud
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Buscar el token en la base de datos y verificar si está activo
            var activeToken = await dbContext.activetokens.FirstOrDefaultAsync(t => t.TokenId == token && t.IsActive);

            if (activeToken == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
