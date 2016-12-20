using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Models
{
    public class UsuarioPesquisa
    {
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string UsuarioPerfil { get; set; }

        public string Descricao { get; set; }

        public string departamentoMasterId { get; set; }
    }
}
