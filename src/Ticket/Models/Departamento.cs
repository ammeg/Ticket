using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Models
{
    [Table("Departamento")]
    public class Departamento : Base
    {
        [Key]
        [Required]
        public Guid DepartamentoId { get; set; }

        [Required(ErrorMessage = "Descrição é OBRIGATÓRIO")]
        [MaxLength(100)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "E-mail é OBRIGATÓRIO")]
        [Display(Name = "E-mail")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Display(Name = "Sigla")]
        public string Sigla { get; set; }

        [Display(Name = "Departamento Master")]
        public Guid? DepartamentoMasterId { get; set; }


    }
}
