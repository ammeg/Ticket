using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Entities
{
    public interface IEntity
    {
        bool Ativo { get; set; }
        DateTime DataHoraCad { get; set; }
        int UsuarioCadId { get; set; }
    }
}
