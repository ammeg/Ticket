using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Entities
{
    [Table("Usuario")]
    public class UsuarioEntity : BaseEntity, IEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Perfil é obrigatório")]
        [Display(Name = "Perfil")]
        public int UsuarioPerfilId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Login é obrigatório")]
        [MaxLength(100)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [MaxLength(100)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [DataType(DataType.Password)]
        [MaxLength(128)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "Mudar Senha nó Próximo Login")]
        public bool MudarSenhaProximoLogin { get; set; }

        [ForeignKey("UsuarioPerfilId")]
        public virtual UsuarioPerfilEntity UsuarioPerfilEntity { get; set; }
    }
}
