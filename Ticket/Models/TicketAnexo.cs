using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Models
{
    [Table("TicketAnexo")]
    public class TicketAnexo : BaseEntity
    {
        [Key]
        [Required]
        public int TicketAnexoId { get; set; }

        [MaxLength(250)]
        [Display(Name = "Caminho do Arquivo")]
        public string CaminhoArquivo { get; set; }

        [Display(Name = "Visibilidade")]
        public int Visibilidade { get; set; }

        [MaxLength(250)]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Required]
        [Display(Name = "Ticket")]
        public int TicketId { get; set; }
    }
}
