using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;
        private readonly IMapper _mapper;

        public PerfilService(IPerfilRepository perfilRepository, IMapper mapper)
        {
            _perfilRepository = perfilRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(PerfilDTO perfilDto)
        {
            var perfil = _mapper.Map<Perfil>(perfilDto);
            await _perfilRepository.AddAsync(perfil);
            perfilDto.IdPerfil = perfil.IdPerfil;
        }

        public async Task Delete(int id)
        {
            await _perfilRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PerfilDTO>> GetAllAsync()
        {
            var perfis = await _perfilRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PerfilDTO>>(perfis);
        }

        public async Task<PerfilDTO> GetByIdAsync(int id)
        {
            var perfil = await _perfilRepository.GetByIdAsync(id);
            return _mapper.Map<PerfilDTO>(perfil);
        }

        public async Task UpdateAsync(PerfilDTO usuario)
        {
            var perfil = _mapper.Map<Perfil>(usuario);
            await _perfilRepository.UpdateAsync(perfil);
        }

        public async Task<PerfilDTO> GetByNomeAsync(string nome)
        {
            var perfil = await _perfilRepository.GetByNomeAsync(nome);
            return _mapper.Map<PerfilDTO>(perfil);
        }
    }
}
