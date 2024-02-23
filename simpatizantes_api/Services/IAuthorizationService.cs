using System.Threading.Tasks;
using simpatizantes_api.DTOs;

namespace simpatizantes_api.DTOs
{
    public interface IAuthorizationService
    {
        Task<AppUserAuthDTO> ValidateUser(AppUserDTO dto);
        Task Logout(int userId);
    }
}