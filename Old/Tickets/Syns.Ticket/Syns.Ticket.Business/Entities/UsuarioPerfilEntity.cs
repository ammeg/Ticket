using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Entities
{
  [Table("UsuarioPerfil")]
    public class UsuarioPerfilEntity : BaseEntity, IEntity
    {
        [Required]
        [Key]
        public int UsuarioPerfilId { get; set; }

        [Required (ErrorMessage= "Descricão Obrigatória")]
        [MaxLength(100)]
        [Display(Name= "Descrição")]
        public string Descricao { get; set; }
    }
}
