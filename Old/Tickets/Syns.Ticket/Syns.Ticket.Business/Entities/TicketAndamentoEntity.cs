using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Syns.Ticket.Business.Entities
{
    [Table("TicketAndamento")]
    public class TicketAndamentoEntity : BaseEntity, IEntity
    {
        [Key]
        public int TicketAndamentoId { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Data Previsão")] // data previsão que vai ser o próximo andamento
        public DateTime? Data { get; set; }

        [Required]
        [Display(Name = "Data Cadastro")]// data hora que foi cadastrado o andamento
        public DateTime? DataHora { get; set; }

        [Display(Name = "Usuário Responsável")] // quem vai resolver
        public int? UsuarioResponsavelId { get; set; }

        [Required]
        [Display(Name = "Visibilidade")] // via programação, publico, interno, externo
        public int Visibilidade { get; set; }

        [Display(Name = "Status")]  //aberto-fechado
        public int Status { get; set; }

        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }

        public int TicketId { get; set; }
    }
}
