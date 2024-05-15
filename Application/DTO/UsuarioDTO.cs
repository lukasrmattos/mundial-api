using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public int IdPerfil { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime Criacao { get; set; }
        public PerfilDTO? Perfil { get; set; }
    }
}
