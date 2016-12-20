using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Ticket.Models
{
    [Table("Sistema")]
    public class Sistema : Base
    {
        [Required]
        [Key]
        public Guid SistemaId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data da Atualização")]
        public DateTime? DataAtualizacao { get; set; }
    }
}
