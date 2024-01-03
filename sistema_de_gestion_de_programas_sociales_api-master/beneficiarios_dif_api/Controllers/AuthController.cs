using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorizationService authService;

        public AuthController(IAuthorizationService authService)
        {
            this.authService = authService;
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
                return StatusCode(500);
            }
        }

    }
}
