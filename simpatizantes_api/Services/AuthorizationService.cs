using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
                              join r in context.Rols on u.Rol.Id equals r.Id
                              where u.Correo == dto.Email && u.Password == dto.Password
                              select new
                              {
                                  User = u,
                                  Rol = r
                              }).FirstOrDefaultAsync();

            if (user != null)
            {
                // Verificar si el usuario tiene una sesión activa
                var existingSession = await context.UserSessions.FirstOrDefaultAsync(s => s.UserId == user.User.Id);
                if (existingSession != null)
                {
                    // Si existe una sesión activa, puedes invalidarla o denegar el inicio de sesión adicional
                    // Puedes invalidarla llamando a algún método de revocación de sesión o simplemente ignorando el inicio de sesión adicional.
                    return null;
                }

                var token = GenerateJwtToken(user);
                var claims = await GetRoleClaims(user.Rol.Id);

                // Guardar la sesión activa en la base de datos
                var newSession = new UserSession { UserId = user.User.Id, Token = token, LastAccessTime = DateTime.UtcNow };
                context.UserSessions.Add(newSession);
                await context.SaveChangesAsync();

                return new AppUserAuthDTO
                {
                    UsuarioId = user.User.Id,
                    NombreCompleto = $"{user.User.Nombre} {user.User.ApellidoPaterno} {user.User.ApellidoMaterno}",
                    Email = user.User.Correo,
                    RolId = user.Rol.Id,
                    Rol = user.Rol.NombreRol,
                    IsAuthenticated = true,
                    Token = token,
                    Claims = claims
                };
            }

            return null;
        }

        private async Task<List<ClaimDTO>> GetRoleClaims(int rolId)
        {
            var claims = await context.Claims.Where(c => c.Rol.Id == rolId).ToListAsync();
            return mapper.Map<List<ClaimDTO>>(claims);
        }

        public string GenerateJwtToken(dynamic user)
        {
            var key = configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new List<System.Security.Claims.Claim>
    {
        new System.Security.Claims.Claim("usuarioId", user.User.Id.ToString()),
        new System.Security.Claims.Claim("nombreCompleto", $"{user.User.Nombre} {user.User.ApellidoPaterno} {user.User.ApellidoMaterno}"),
        new System.Security.Claims.Claim("rolId", user.Rol.Id.ToString()),
        new System.Security.Claims.Claim("rol", user.Rol.NombreRol),
    };

            var claimsIdentity = new ClaimsIdentity(claims);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenConfig);
        }

        public async Task Logout(int userId)
        {
            // Eliminar la sesión activa del registro de sesiones
            var sessionToRemove = await context.UserSessions.FirstOrDefaultAsync(s => s.UserId == userId);
            if (sessionToRemove != null)
            {
                context.UserSessions.Remove(sessionToRemove);
                await context.SaveChangesAsync();
            }
        }
    }
}
