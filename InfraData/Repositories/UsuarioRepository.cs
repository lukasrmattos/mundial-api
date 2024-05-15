using Domain.Entity;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);

            if(usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return Task.FromResult(_context.Usuarios.Include(u => u.Perfil).AsEnumerable());
        }

        public Task<Usuario> GetByIdAsync(int id)
        {
            return _context.Usuarios.Include(u => u.Perfil).FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<Usuario> GetByEmailAndSenhaAsync(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }

        public Task<Usuario> GetByEmailAsync(string email)
        {
            return _context.Usuarios.Include(u => u.Perfil).FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            return _context.SaveChangesAsync();
        }
    }
}
