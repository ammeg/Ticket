using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Models
{
    [Table("UsuarioPerfil")]
    public class UsuarioPerfil : Base
    {
        [Required]
        [Key]
        public Guid UsuarioPerfilId { get; set; }

        [Required (ErrorMessage= "Descricão Obrigatória")]
        [MaxLength(100)]
        [Display(Name= "Descrição")]
        public string Descricao { get; set; }
    }
}
