using AutoMapper;
using simpatizantes_api.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace simpatizantes_api.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthorizationService(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            this.context = context;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<AppUserAuthDTO> ValidateUser(AppUserDTO dto)
        {
            var user = await (from u in context.Usuarios
                                join r in context.Rols
                                on u.Rol.Id equals r.Id
                                where u.Correo == dto.Email && u.Password == dto.Password
                                select new AppUserAuthDTO
                                {
                                    UsuarioId = u.Id,
                                    NombreCompleto = $"{u.Nombre} {u.ApellidoPaterno} {u.ApellidoMaterno}",
                                    Email = u.Correo,
                                    RolId = r.Id,
                                    Rol = r.NombreRol,
                                }).FirstOrDefaultAsync();

            if (user != null)
            {
                user.IsAuthenticated = true;
                user.Token = GenerateJwtToken(user);
                user.Claims = await GeRolClaims(user.RolId);
            }

            return user;
        }


        private async Task<List<ClaimDTO>> GeRolClaims(int rolId)
        {
            var claims = await context.Claims.Where(c => c.Rol.Id == rolId).ToListAsync();
            return mapper.Map<List<ClaimDTO>>(claims);
        }

        public string GenerateJwtToken(AppUserAuthDTO dto)
        {
            var key = configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key); 

            // Describe las propiedades del usuario
            var claims = new List<Claim>
            {
                new Claim("usuarioId", dto.UsuarioId.ToString()),
                new Claim("nombreCompleto", dto.NombreCompleto),
                new Claim("rolId", dto.RolId.ToString()),
                new Claim("rol", dto.Rol),

                // Puedes agregar más claims personalizados según tus necesidades
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            // Encripta la credencial de los tokens en a la clave en bytes 
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            // Describe el token en base a la propiedades, expiracion y la credencial
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = credentials
            };

            // Se cra un nuevo token a manipular instanciado de Jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            // Se escribe el nuevo token manipulado en base a las propiedades y su configuracion
            return tokenHandler.WriteToken(tokenConfig);
        }





    }
}
