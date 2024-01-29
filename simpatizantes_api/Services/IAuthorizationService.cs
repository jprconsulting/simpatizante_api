using simpatizantes_api.DTOs;

namespace simpatizantes_api.Services
{
    public interface IAuthorizationService
    {
        Task<AppUserAuthDTO> ValidateUser(AppUserDTO dto);
    }
}
