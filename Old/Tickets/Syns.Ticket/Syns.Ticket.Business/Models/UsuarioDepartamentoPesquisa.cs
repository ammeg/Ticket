using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Models
{
    public class UsuarioDepartamentoPesquisa
    {
        public int UsuarioId { get; set; }

        public int DepartamentoId { get; set; }

        public string Usuario { get; set; }

        public bool Responsavel { get; set; }

        public string Departamento { get; set; }

        public string ResponsavelS
        {
            get
            {
                if (this.Responsavel == true)
                    return "Sim";
                else return "Não";
            }
        }
    }
}
