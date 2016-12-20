using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Models
{
    public class TicketAnexo
    {
        public int Ticket { get; set; }

        public string Observacao { get; set; }

        public string Caminho { get; set; }

        public int TicketId { get; set; }

        public int TicketAnexoId { get; set; }

        public string CaminhoArquivo { get; set; }
    }
}
