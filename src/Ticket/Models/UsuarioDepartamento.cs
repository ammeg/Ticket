using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ticket.Models
{
    [Table("UsuarioDepartamento")]
    public class UsuarioDepartamento : Base
    {
        [Key]
        [Required]
        [Column(Order = 0)]
        [Display(Name = "Usuário Responsável")]
        public Guid UsuarioId { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Departamento Responsável")]
        public Guid DepartamentoId { get; set; }

        [Required]
        [Display(Name = "Responsável")]
        public bool Responsavel { get; set; }
    }
}
