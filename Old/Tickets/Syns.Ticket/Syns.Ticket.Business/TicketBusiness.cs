using Syns.Ticket.Business.Entities;
using Syns.Ticket.Business.Models;
using Syns.Ticket.Lib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business
{
    public class TicketBusiness : BaseBusiness
    {
        public static TicketEntity Create(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.TicketEntity.FirstOrDefault(a => a.TicketId == id);
            }
        }

        public static List<TicketEntity> Pesquisar()
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.TicketEntity.ToList();
            }
        }

        public void Salvar(TicketEntity entity, int usuarioId)
        {
            try
            {
                using (SynsTicketContext context = new SynsTicketContext())
                {
                    entity.DataCadastro = DateTime.Now;

                    var departamento = DepartamentoBusiness.Create(entity.DepartamentoId);

                    entity.UsuarioId = usuarioId;
                    entity.UsuarioResponsavelId = null;
                    entity.Visibilidade = 1;
                    entity.DataPrevisao = null;

                    base.SaveChanges(entity, usuarioId, true, context);
                }
            }
            catch (TicketException ex)
            {
                this.Erros.Add(ex);
            }
            finally
            {
                if (this.Erros.Count > 0)
                    throw new TicketException(this.ConsisteErros());
            }
        }

        protected override bool ConsisteInsertOrUpdate(IEntity entity, DbContext context)
        {
            var ticketContext = (SynsTicketContext)context;
            var ticketEntity = (TicketEntity)entity;

            var ticketDuplicado = from a in ticketContext.TicketEntity
                                  where a.TicketId != ticketEntity.TicketId
                                         && a.TicketId.Equals(ticketEntity.TicketId)
                                  select a;

            if (ticketDuplicado.Count() > 0)
                this.Erros.Add(new TicketException("Ticket já existe"));

            return base.ConsisteInsertOrUpdate(entity, context);
        }


        public void CancelarTicket(TicketEntity entity, int usuarioId)
        {
            try
            {
                using (SynsTicketContext context = new SynsTicketContext())
                {

                    base.SaveChanges(entity, usuarioId, true, context);
                }
            }
            catch (TicketException ex)
            {
                this.Erros.Add(ex);
            }
            finally
            {
                if (this.Erros.Count > 0)
                    throw new TicketException(this.ConsisteErros());
            }
        }


        public void Excluir(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                base.Remove<TicketEntity>(context, id);
            }
        }

        //public static List<TicketPesquisa> PesquisarTicketPorId(int ticketId)
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var dados = from ticket in context.TicketEntity 
        //                    where ticket.TicketId == ticketId
        //                    select new TicketPesquisa()
        //                    {
        //                        TicketId = ticket.TicketId,

        //                    };
        //        return dados.ToList();
        //    }
        //}
        public static List<TicketAnexoPesquisa> PesquisarAnexoPorTicket(int ticketId)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = from anexo in context.TicketAnexoEntity
                            where anexo.TicketId == ticketId

                            select new TicketAnexoPesquisa()
                            {
                                TicketId = anexo.TicketId,
                                Observacao = anexo.Observacao,
                                CaminhoArquivo = anexo.CaminhoArquivo,
                                TicketAnexoId = anexo.TicketAnexoId
                            };

                return dados.ToList();
            }
        }

        public static List<TicketAndamentoPesquisa> PesquisarAndamentoPorTicket(int ticketId, int usuarioId)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = (from andamento in context.TicketAndamentoEntity
                             where andamento.TicketId == ticketId
                             orderby andamento.Data descending
                             select new TicketAndamentoPesquisa()
                             {
                                 TicketId = andamento.TicketId,
                                 TicketAndamentoId = andamento.TicketAndamentoId,
                                 Status = andamento.Status,
                                 DepartamentoId = andamento.DepartamentoId,
                                 Descricao = andamento.Descricao,
                                 Visibilidade = andamento.Visibilidade
                             }).ToList();

                var andamentosPublicos = dados.Where(a => a.Visibilidade == 1);

                var andamentosPrivados = dados.Where(a => a.Visibilidade == 0 && a.UsuarioId == usuarioId);

                var andamentos = andamentosPublicos.Union(andamentosPrivados).OrderByDescending(a => a.Data);

                return andamentos.ToList();
            }
        }
        public static List<TicketPesquisa> PesquisarTodosTicket(int usuarioId)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = from ticket in context.TicketEntity
                            join usuario in context.UsuarioEntity on ticket.UsuarioId equals usuario.UsuarioId
                            join usuarioDepartamento in context.UsuarioDepartamentoEntity on usuario.UsuarioId equals usuarioDepartamento.UsuarioId
                            join departamento in context.DepartamentoEntity on usuarioDepartamento.DepartamentoId equals departamento.DepartamentoId

                            where usuarioDepartamento.UsuarioId == usuarioId

                            select new TicketPesquisa()
                                        {
                                            TicketId = ticket.TicketId,
                                            DepartamentoId = ticket.DepartamentoId,
                                            UsuarioResponsavel = ticket.UsuarioResponsavelId,
                                            Visivilidade = ticket.Visibilidade,
                                            Departamento = departamento.Descricao,
                                            UsuarioId = ticket.UsuarioId,
                                            Assunto = ticket.Assunto,
                                            Prioridade = ticket.Prioridade,
                                            Ticket2Id = ticket.TicketId,
                                            Ticket3Id = ticket.TicketId,
                                            Ticket4Id = ticket.TicketId,

                                        };

                return dados.ToList();
            }
        }

        //public static List<TicketAndamentoPesquisa> PesquisarAndamentosTicket()
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var dados = from ticket in context.TicketAndamentoEntity
        //                    select new TicketPesquisa()
        //                    {
        //                        TicketId = TicketAndamentoPesquisa

        //                    };
        //        return dados.ToList();
        //    }
        //}


    }
}
