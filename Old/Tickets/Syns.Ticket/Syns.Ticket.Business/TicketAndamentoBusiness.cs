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
    public class TicketAndamentoBusiness : BaseBusiness
    {
        public static List<TicketAndamentoEntity> PesquisarTodos()
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.TicketAndamentoEntity.ToList();
            }
        }


        public static TicketAndamentoEntity Create(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.TicketAndamentoEntity.FirstOrDefault(a => a.TicketAndamentoId == id);
            }
        }
        public static List<TicketAndamentoPesquisa> Pesquisar(string nome, string sortBy, int startIndex, int pageSize, out int count)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = PesquisarQuery(nome, context);

                return dados.OrderBy(sortBy).Page(startIndex, pageSize, out count);
            }
        }

        public static List<TicketAndamentoPesquisa> Pesquisar(string nome)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = PesquisarQuery(nome, context);

                return dados.ToList();
            }
        }

        private static IQueryable<TicketAndamentoPesquisa> PesquisarQuery(string nome, SynsTicketContext context)
        {
            var dados = from andamento in context.TicketAndamentoEntity 
                        join usuario in context.UsuarioEntity on andamento.UsuarioResponsavelId equals usuario.UsuarioId
                        join usuarioPerfil in context.UsuarioPerfilEntity on usuario.UsuarioPerfilId equals usuarioPerfil.UsuarioPerfilId
                        join andamentoTicket in context.TicketEntity on andamento.TicketId equals andamentoTicket.TicketId
                        join departamento in context.DepartamentoEntity on andamento.DepartamentoId equals departamento.DepartamentoId
                        orderby andamento.TicketAndamentoId
                        select new TicketAndamentoPesquisa()
                        {
                            UsuarioId = usuario.UsuarioId,
                           // Nome = usuario.Nome,
                           // Login = usuario.Login,
                         //   UsuarioPerfil = usuarioPerfil.Descricao,
                          
                            UsuarioResponsavel = usuario.Nome,
                            UsuarioCadastro = usuario.Nome,
                            Departamento = departamento.Descricao,
                            DataHora = andamento.DataHora,
                            Data = andamento.Data,

                        };

            if (!string.IsNullOrWhiteSpace(nome))
            {
                dados = dados.Where(a => a.Nome.Contains(nome));
            }
            return dados;
        }

        protected override bool ConsisteInsertOrUpdate(Entities.IEntity entity, DbContext context)
        {
            var ticketAndamentoContext = (SynsTicketContext)context;
            var ticketAdamentoEntity = (TicketAndamentoEntity)entity;


            return base.ConsisteInsertOrUpdate(entity, context);
        }


        public void Salvar(TicketAndamentoEntity entity, int usuarioId)
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
                base.Remove<TicketAndamentoEntity>(context, id);
            }
        }

    }
}
