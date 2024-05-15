using Application.DTO;
using Domain.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUsuarioService
    {
        Task<string> AuthenticateAsync(AuthDTO login);
        Task AddAsync(UsuarioDTO usuarioDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO> GetByIdAsync(int id);
        Task UpdateAsync(UsuarioDTO usuarioDto);
        Task<UsuarioDTO> GetByEmailAndSenhaAsync(string email, string senha);
        Task<UsuarioDTO> GetByEmail(string email);
    }
}
