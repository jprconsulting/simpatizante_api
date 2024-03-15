using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using simpatizantes_api.DTOs;
using simpatizantes_api.Entities;
using simpatizantes_api.Exceptions;
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

        private async Task InvalidatePreviousTokens(int userId)
        {
            var previousTokens = await context.ActiveTokens
                .Where(t => t.UserId == userId && t.IsActive)
                .ToListAsync();

            foreach (var token in previousTokens)
            {
                token.IsActive = false;
                token.ExpirationTime = DateTime.UtcNow; // Marcar el tiempo de expiración como el tiempo actual para que el token esté expirado
            }

            await context.SaveChangesAsync();
        }

        public async Task<AppUserAuthDTO> ValidateUser(AppUserDTO dto)
        {
            var user = await context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == dto.Email && u.Password == dto.Password);

            if (user != null)
            {
                // Invalidar los tokens anteriores del usuario
                await InvalidatePreviousTokens(user.Id);

                // Generar un nuevo token JWT
                var token = GenerateJwtToken(user);

                // Guardar el tokenId activo en la base de datos
                var activeToken = new ActiveToken { TokenId = token, UserId = user.Id, IsActive = true };
                context.ActiveTokens.Add(activeToken);
                await context.SaveChangesAsync();

                // Resto del código para devolver la información del usuario con el nuevo token
                var claims = await GetRoleClaims(user.Rol.Id);
                return new AppUserAuthDTO
                {
                    UsuarioId = user.Id,
                    NombreCompleto = $"{user.Nombre} {user.ApellidoPaterno} {user.ApellidoMaterno}",
                    Email = user.Correo,
                    RolId = user.Rol.Id,
                    Rol = user.Rol.NombreRol,
                    IsAuthenticated = true,
                    Token = token,
                    Claims = claims,
                    CandidatoId = user.Rol.Id == 3 ? user.CandidatoId : null,
                    OperadorId = user.Rol.Id == 2 ? user.OperadorId : null,
                };
            }

            // Si el usuario no fue encontrado, lanzar una excepción 
            throw new UnauthorizedAccessException("Correo o contraseña incorrectos");
        }

        private async Task<List<ClaimDTO>> GetRoleClaims(int rolId)
        {
            var claims = await context.Claims.Where(c => c.Rol.Id == rolId).ToListAsync();
            return mapper.Map<List<ClaimDTO>>(claims);
        }

        public string GenerateJwtToken(Usuario user)
        {
            var key = configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            // Generar un identificador único para el token
            var tokenId = Guid.NewGuid().ToString();

            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim("usuarioId", user.Id.ToString()),
                new System.Security.Claims.Claim("nombreCompleto", $"{user.Nombre} {user.ApellidoPaterno} {user.ApellidoMaterno}"),
                new System.Security.Claims.Claim("rolId", user.Rol.Id.ToString()),
                new System.Security.Claims.Claim("rol", user.Rol.NombreRol),
                new System.Security.Claims.Claim("tokenId", tokenId), // Incluir el tokenId en los claims
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(24), // Expiración del token
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenConfig);
        }

        public async Task Logout(int userId)
        {
            // Aquí podrías implementar la lógica para cerrar sesión si es necesario
            // Por ejemplo, podrías invalidar el token del usuario en la base de datos
            // o eliminar cualquier registro relacionado con la sesión activa del usuario
        }
    }
}
