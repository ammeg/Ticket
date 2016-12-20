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
    public class TicketAnexoBusiness : BaseBusiness
    {
        protected override bool ConsisteInsertOrUpdate(Entities.IEntity entity, DbContext context)
        {
            var ticketContext = (SynsTicketContext)context;
            var ticketAnexoEntity = (TicketAnexoEntity)entity;

            var usarioLoginIguais = from a in ticketContext.TicketAnexoEntity
                                    where a.TicketAnexoId != ticketAnexoEntity.TicketAnexoId
                                           && a.CaminhoArquivo.Equals(ticketAnexoEntity.CaminhoArquivo)
                                    select a;



            //if (usarioLoginIguais.Count() > 0)
            //    this.Erros.Add(new TicketException("Anexo com problemas, id ou caminhos iguais a outro anexo"));


            return base.ConsisteInsertOrUpdate(entity, context);
        }

        public void Excluir(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                base.Remove<TicketAnexoEntity>(context, id);
            }
        }

        public void Salvar(TicketAnexoEntity entity, int ticketAnexoId)
        {
            try
            {
                using (SynsTicketContext context = new SynsTicketContext())
                {
                    base.SaveChanges(entity, ticketAnexoId, true, context);
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
        public static List<TicketAnexoPesquisa> PesquisarTicketPorAnexo(int ticketId)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = from anexo in context.TicketAnexoEntity
                            where anexo.TicketId == ticketId
                            select new TicketAnexoPesquisa()
                            {
                                TicketId = anexo.TicketId,
                                CaminhoArquivo = anexo.CaminhoArquivo,
                                Observacao = anexo.Observacao,
                                TicketAnexoId = anexo.TicketAnexoId

                            };
                return dados.ToList();
            }
        }
        //private static List<TicketAnexo> PesquisarQuery(int ticketId)
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var dados = from anexo in context.TicketAnexoEntity
        //                    join ticket in context.TicketEntity on anexo.TicketId equals ticket.TicketId
        //                    where anexo.TicketId == ticketId
        //                    select new TicketAnexoPesquisa()
        //                    {
        //                        TicketId = anexo.TicketId,
        //                        TicketAnexoId = anexo.TicketAnexoId,
        //                        Observacao = anexo.Observacao,
        //                        CaminhoArquivo = anexo.CaminhoArquivo,
        //                    };


        //        return dados.ToList();
        //    }
        //}

    }
}
