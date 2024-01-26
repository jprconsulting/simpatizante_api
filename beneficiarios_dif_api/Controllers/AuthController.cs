using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace conoceles_api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorizationService authService;
        private readonly ILogger<AuthController> logger;

        public AuthController(IAuthorizationService authService, ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AppUserDTO dto)
        {
            try
            {
                var result = await authService.ValidateUser(dto);
                if (result == null)
                    return Unauthorized();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Loguea la excepción para obtener más detalles
                logger.LogError(ex, "Error durante la autenticación del usuario.");

                // Devuelve una respuesta con detalles del error
                return StatusCode(500, new { ErrorMessage = "Ocurrió un error durante la autenticación del usuario.", Exception = ex.Message });
            }
        }
    }
}