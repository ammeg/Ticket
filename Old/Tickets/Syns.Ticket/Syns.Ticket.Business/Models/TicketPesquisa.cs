using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Models
{
    public class TicketPesquisa
    {
        public int TicketId { get; set; }

        public string CaminhoArquivo { get; set; }

        public string Observacao { get; set; }

        public int TicketAnexoId { get; set; }

        public string Departamento { get; set; }

        public int? UsuarioResponsavel { get; set; }

        public int Visivilidade { get; set; }

        public int DepartamentoId { get; set; }

        public int UsuarioId { get; set; }

        public string Assunto { get; set; }

        public int Prioridade { get; set; }

        public int Categoria { get; set; }

        public int Usuario { get; set; }

        public string departamento { get; set; }

        public string usuario { get; set; }

        public string usuarioResponsavel { get; set; }

        public int Ticket2Id { get; set; }

        public int Ticket3Id { get; set; }

        public int Ticket4Id { get; set; }
    }
}
