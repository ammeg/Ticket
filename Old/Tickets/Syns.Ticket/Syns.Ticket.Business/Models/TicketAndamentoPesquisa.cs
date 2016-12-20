using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Models
{
    public class TicketAndamentoPesquisa
    {
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string UsuarioPerfil { get; set; }

        public string Ticket { get; set; }

        public string UsuarioResponsavel { get; set; }

        public string UsuarioCadastro { get; set; }

        public string Departamento { get; set; }

        public DateTime? DataHora { get; set; }

        public DateTime? Data { get; set; }

        public int TicketId { get; set; }

        public int Status { get; set; }

        public int TicketAndamentoId { get; set; }

        public string Descricao { get; set; }

        public int DepartamentoId { get; set; }

        public int Visibilidade { get; set; }
    }
}
