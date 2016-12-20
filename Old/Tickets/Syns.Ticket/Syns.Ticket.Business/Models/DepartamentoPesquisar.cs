using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Models
{
    public class DepartamentoPesquisar
    {
        public string Descricao { get; set; }

        public int DepartamentoId { get; set; }

        public string DepartamentoMaster { get; set; }

        public string Sigla { get; set; }

        public int? DepartamentoMasterId { get; set; }

        public string DescricaoCompleta { get; set; }
    }
}
