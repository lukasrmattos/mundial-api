using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUnitOfWork uow, IMapper mapper, IConfiguration configuration)
        {
            _uow = uow;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(AuthDTO login)
        {
            var user = await this.GetByEmailAndSenhaAsync(login.Email, login.Senha);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfigurations:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task AddAsync(UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);

            // criptografando a senha
            string senha_md5 = string.Empty;

            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(usuario.Senha));
                senha_md5 = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            usuario.Senha = senha_md5;

            await _uow.Usuarios.AddAsync(usuario);
            usuarioDto.Id = usuario.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _uow.Usuarios.GetByIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            await _uow.Usuarios.DeleteAsync(id);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _uow.Usuarios.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            var usuario = await _uow.Usuarios.GetByIdAsync(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task UpdateAsync(UsuarioDTO usuarioDto)
        {
            var usuario = await _uow.Usuarios.GetByIdAsync(usuarioDto.Id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            usuario = _mapper.Map<Usuario>(usuarioDto);
            await _uow.Usuarios.Update(usuario);
        }

        public async Task<UsuarioDTO> GetByEmailAndSenhaAsync(string email, string senha)
        {
            string senha_md5_decrypt = string.Empty;

            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(senha));
                senha_md5_decrypt = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            var usuario = await _uow.Usuarios.GetByEmailAndSenhaAsync(email, senha_md5_decrypt);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> GetByEmail(string email)
        {
            var usuario = await _uow.Usuarios.GetByEmailAsync(email);
            return _mapper.Map<UsuarioDTO>(usuario);
        }
    }
}
