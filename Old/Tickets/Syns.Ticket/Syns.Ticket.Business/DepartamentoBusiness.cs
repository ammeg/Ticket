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
    public class DepartamentoBusiness : BaseBusiness
    {
        public static DepartamentoEntity Create(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.DepartamentoEntity.FirstOrDefault(a => a.DepartamentoId == id);
            }
        }

        public static List<DepartamentoPesquisar> Pesquisar(string descricao, string sortBy, int startIndex, int pageSize, out int count)
        {

            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = from departamento in context.DepartamentoEntity
                            where departamento.Descricao.Contains(descricao)
                            orderby departamento.Descricao
                            select new DepartamentoPesquisar()
                            {
                                DepartamentoId = departamento.DepartamentoId,
                                Descricao = departamento.Descricao,
                                Sigla = departamento.Sigla,
                                DepartamentoMasterId = departamento.DepartamentoMasterId
                            };

                if (!string.IsNullOrWhiteSpace(descricao))
                {
                    dados = dados.Where(a => a.Descricao.Contains(descricao));
                }

                List<DepartamentoPesquisar> itens = new List<DepartamentoPesquisar>();

                foreach (var dado in dados)
                {
                    DepartamentoPesquisar item = MontarItem(dado);

                    itens.Add(item);
                }

                return itens.OrderBy(a => a.DescricaoCompleta).OrderBy2(sortBy).Page(startIndex, pageSize, out count);
            }
        }

        private static DepartamentoPesquisar MontarItem(DepartamentoPesquisar dado)
        {
            
                dado.DescricaoCompleta = MontarItenDescricaoCompleta(dado.DepartamentoId);

            

            return dado;
        }

        private static string MontarItenDescricaoCompleta(int departamentoId)
        {
            string descricaoCompleta = "";
            using (SynsTicketContext context = new SynsTicketContext())
            {
                DepartamentoEntity departamento = null;
                do
                {
                    departamento = context.DepartamentoEntity.FirstOrDefault(a => a.DepartamentoId == departamentoId);

                    if (departamento.DepartamentoMasterId.HasValue)
                    {
                        departamentoId = departamento.DepartamentoMasterId.Value;
                        if (String.IsNullOrWhiteSpace(descricaoCompleta))
                            descricaoCompleta = departamento.Descricao;
                        else descricaoCompleta = departamento.Descricao + " | " + descricaoCompleta;
                    }
                    else
                    {
                        if (departamento != null)
                        {
                            if (String.IsNullOrWhiteSpace(descricaoCompleta))
                                descricaoCompleta = departamento.Descricao;
                            else descricaoCompleta = departamento.Descricao + " | " + descricaoCompleta;
                        }
                        departamentoId = 0;
                        departamento = null;
                    }
                }
                while (departamento != null && departamentoId != 0);

            }

            return descricaoCompleta;
        }

        public static List<DepartamentoEntity> PesquisarDescricao(string descricao)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = (from departamento in context.DepartamentoEntity
                             where departamento.Descricao.Contains(descricao)
                             orderby departamento.Descricao
                             select departamento).ToList();

                return dados;
            }
        }

        public void Salvar(DepartamentoEntity entity, int departamentoId)
        {
            try
            {
                using (SynsTicketContext context = new SynsTicketContext())
                {
                    base.SaveChanges(entity, departamentoId, true, context);
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
            var departamentoEntity = (DepartamentoEntity)entity;

            var descricaoDuplicada = from a in ticketContext.DepartamentoEntity
                                     where a.DepartamentoId != departamentoEntity.DepartamentoId
                                            && a.Descricao.Equals(departamentoEntity.Descricao)
                                     select a;

            var siglaDuplicada = from a in ticketContext.DepartamentoEntity
                                 where a.DepartamentoId != departamentoEntity.DepartamentoId
                                        && a.Sigla.Equals(departamentoEntity.Sigla)
                                 select a;

            var emailDuplicada = from a in ticketContext.DepartamentoEntity
                                 where a.DepartamentoId != departamentoEntity.DepartamentoId
                                        && a.Email.Equals(departamentoEntity.Email)
                                 select a;

            if (descricaoDuplicada.Count() > 0)
                this.Erros.Add(new TicketException("Campo descrição está duplicada"));
            if (siglaDuplicada.Count() > 0)
                this.Erros.Add(new TicketException("Campo Sigla está duplicada"));
            if (emailDuplicada.Count() > 0)
                this.Erros.Add(new TicketException("Campo E-mail está duplicado"));

            return base.ConsisteInsertOrUpdate(entity, context);
        }

        public void Excluir(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                base.Remove<DepartamentoEntity>(context, id);
            }
        }



        public static List<DepartamentoEntity> PesquisarNovo(string descricao, string sortBy, int startIndex, int pageSize, out int count)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = (from up in context.DepartamentoEntity
                             where up.Descricao.Contains(descricao)
                             select up);

                return dados.OrderBy(sortBy).Page(startIndex, pageSize, out count);
            }
        }
    }
}