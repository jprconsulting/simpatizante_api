using beneficiarios_dif_api.DTOs;

namespace beneficiarios_dif_api.Services
{
    public interface IAuthorizationService
    {
        Task<AppUserAuthDTO> ValidateUser(AppUserDTO dto);
    }
}
