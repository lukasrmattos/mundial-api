using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPerfilRepository
    {
        Task AddAsync(Perfil perfil);
        Task DeleteAsync(int id);
        Task<IEnumerable<Perfil>> GetAllAsync();
        Task<Perfil> GetByIdAsync(int id);
        Task UpdateAsync(Perfil perfil);
        Task<Perfil> GetByNomeAsync(string nome);
    }
}
