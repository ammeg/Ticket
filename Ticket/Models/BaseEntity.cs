using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Models
{
    public class BaseEntity : IEntity
    {
        public string UsuarioCad { get; set; }
     
        [Required]
        public DateTime DataHoraCad { get; set; }
        
        [Required]
        public bool Ativo { get; set; }
    }
}
