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
    public class PerfilRepository : IPerfilRepository
    {
        private readonly AppDbContext _context;

        public PerfilRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Perfil perfil)
        {
            _context.Perfis.Add(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var perfil = await GetByIdAsync(id);

            if(perfil != null)
            {
                _context.Perfis.Remove(perfil);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Perfil>> GetAllAsync()
        {
            return await _context.Perfis.Include(p => p.Usuarios).ToListAsync();
        }

        public async Task<Perfil> GetByIdAsync(int id)
        {
            return await _context.Perfis.Include(p => p.Usuarios).FirstOrDefaultAsync(p => p.IdPerfil == id);
        }

        public async Task UpdateAsync(Perfil perfil)
        {
            _context.Perfis.Update(perfil);
            await _context.SaveChangesAsync();
        }

        public async Task<Perfil> GetByNomeAsync(string nome)
        {
            return await _context.Perfis.Include(p => p.Usuarios).FirstOrDefaultAsync(p => p.Nome == nome);
        }
    }
}
