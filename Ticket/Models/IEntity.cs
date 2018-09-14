using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Models
{
    public interface IEntity
    {
        bool Ativo { get; set; }
        DateTime DataHoraCad { get; set; }
        string UsuarioCad { get; set; }
    }
}
