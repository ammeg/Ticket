using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Models
{
    [Table("Ticket")]
    public class Tickets : Base
    {
        [Key]
        [Required]
        public Guid TicketId { get; set; }

        [Display(Name = "Ticket Master")]
        public Guid? TicketMasterId { get; set; } // Caso o ticket faça referencia a outro ticket que trate do mesmo assunto

        [Required(ErrorMessage = "Categoria é OBRIGATÓRIO")]
        [Display(Name = "Categoria")]
        public Guid CategoriaId { get; set; } // Não faz referencia com nenhuma outra tabela, será colocado via programação - Erro - Dúvida- Problema

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
        public Guid DepartamentoId { get; set; } // financeiro - TI - faturamento ...

        // FK de Usuário
        [Display(Name = "Usuário Responsável")]
        public Guid? UsuarioResponsavelId { get; set; }

        // FK de Usuário
        [Display(Name = "Solicitante")]
        public Guid UsuarioId { get; set; }
    }
}
