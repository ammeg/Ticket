using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Models
{
    [Table("TicketAnexo")]
    public class TicketAnexo : Base
    {
        [Key]
        [Required]
        public Guid TicketAnexoId { get; set; }

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
        public Guid TicketId { get; set; }
    }
}
