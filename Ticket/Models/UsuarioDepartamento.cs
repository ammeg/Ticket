using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ticket.Models
{
    [Table("UsuarioDepartamento")]
    public class UsuarioDepartamento : BaseEntity
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        [Display(Name = "Usuário Responsável")]
        public int UsuarioId { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Departamento Responsável")]
        public int DepartamentoId { get; set; }

        [Required]
        [Display(Name = "Responsável")]
        public bool Responsavel { get; set; }
    }
}
