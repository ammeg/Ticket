using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Syns.Ticket.ClientMvc.Models
{
    public class UsuarioLoginModel
    {
        [Required(ErrorMessage = "Senha é obrigatório")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [DataType(DataType.Password)]
        [MaxLength(128)]
        public string Senha { get; set; }
    }
}