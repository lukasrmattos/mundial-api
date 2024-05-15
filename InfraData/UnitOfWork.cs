using Domain.Repositories;
using InfraData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraData
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private UsuarioRepository _usuarioRepository;
        private PerfilRepository _perfilRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUsuarioRepository Usuarios => _usuarioRepository ??= new UsuarioRepository(_context);
        public IPerfilRepository Perfis => _perfilRepository ??= new PerfilRepository(_context);
        
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
