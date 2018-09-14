using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Models
{
    [Table("Departamento")]
    [DisplayColumn("Descricao")]
    public class Departamento : BaseEntity
    {
        [Key]
        [Required]
        public int DepartamentoId { get; set; }

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
        public int? DepartamentoMasterId { get; set; }


    }
}
