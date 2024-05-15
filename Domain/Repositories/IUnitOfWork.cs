using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        IUsuarioRepository Usuarios { get; }
        IPerfilRepository Perfis { get; }
        Task<int> CommitAsync();
    }
}
