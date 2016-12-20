using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business.Entities
{
    [Table("Ticket")]
    public class TicketEntity : BaseEntity, IEntity
    {
        [Key]
        [Required]
        public int TicketId { get; set; }

        [Display(Name = "Ticket Master")]
        public int? TicketMasterId { get; set; } // Caso o ticket faça referencia a outro ticket que trate do mesmo assunto

        [Required(ErrorMessage = "Categoria é OBRIGATÓRIO")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; } // Não faz referencia com nenhuma outra tabela, será colocado via programação - Erro - Dúvida- Problema

        [Required(ErrorMessage = "Descrição é OBRIGATÓRIO")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy - hh/mm/ss}")]
        public DateTime? DataCadastro { get; set; }

        [Required(ErrorMessage = "Assunto é OBRIGATÓRIO")]
        [MaxLength(255)]
        [Display(Name = "Assunto")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Visibilidade é OBRIGATÓRIO")]
        [Display(Name = "Visibilidade")]
        public int Visibilidade { get; set; } // Via programação - protegido - privado e público

        [Display(Name = "Data Previsão")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataPrevisao { get; set; } // para a resolução do problema ou para o próximo processo do ticket

               [Required]
        [Display(Name = "Prioridade")]
        public int Prioridade { get; set; }

        // *************************************************************************
        // *********** Chaves Extrangeiras que estão na tabela Ticket **************
        // *************************************************************************
        // FK de Departamento
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; } // financeiro - TI - faturamento ...

        // FK de Usuário
        [Display(Name = "Usuário Responsável")]
        public int? UsuarioResponsavelId { get; set; }

        // FK de Usuário
        [Display(Name = "Solicitante")]
        public int UsuarioId { get; set; }
    }
}
