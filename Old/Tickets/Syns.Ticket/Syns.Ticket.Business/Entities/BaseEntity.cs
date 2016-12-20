using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Entities
{
    public class BaseEntity
    {
        [Required]
        public int UsuarioCadId { get; set; }
     
        [Required]
        public DateTime DataHoraCad { get; set; }
        
        [Required]
        public bool Ativo { get; set; }
    }
}
