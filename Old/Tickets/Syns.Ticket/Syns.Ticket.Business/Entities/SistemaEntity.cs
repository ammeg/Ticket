using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Syns.Ticket.Business.Entities
{
    [Table("Sistema")]
    public class SistemaEntity : BaseEntity
    {
        [Required]
        [Key]
        public int SistemaId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data da Atualização")]
        public DateTime? DataAtualizacao { get; set; }
    }
}
