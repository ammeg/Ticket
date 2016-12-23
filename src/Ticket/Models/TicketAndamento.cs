using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ticket.Models
{
    [Table("TicketAndamento")]
    public class TicketAndamento : Base
    {
        [Key]
        public Guid TicketAndamentoId { get; set; }

        [Required]
       // [AllowHtml]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Data Previsão")] // data previsão que vai ser o próximo andamento
        public DateTime? Data { get; set; }

        [Required]
        [Display(Name = "Data Cadastro")]// data hora que foi cadastrado o andamento
        public DateTime? DataHora { get; set; }

        [Display(Name = "Usuário Responsável")] // quem vai resolver
        public Guid? UsuarioResponsavelId { get; set; }

        [Required]
        [Display(Name = "Visibilidade")] // via programação, publico, interno, externo
        public int Visibilidade { get; set; }

        [Display(Name = "Status")]  //aberto-fechado
        public int Status { get; set; }

        [Display(Name = "Departamento")]
        public Guid DepartamentoId { get; set; }

        public Guid TicketId { get; set; }
    }
}
