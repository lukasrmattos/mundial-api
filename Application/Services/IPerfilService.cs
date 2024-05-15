using Application.DTO;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPerfilService
    {
        Task AddAsync(PerfilDTO perfilDto);
        Task Delete(int id);
        Task<IEnumerable<PerfilDTO>> GetAllAsync();
        Task<PerfilDTO> GetByIdAsync(int id);
        Task UpdateAsync(PerfilDTO usuario);
        Task<PerfilDTO> GetByNomeAsync(string nome);
    }
}
