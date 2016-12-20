using Syns.Ticket.Business.Entities;
using Syns.Ticket.Lib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business
{
    public class UsuarioPerfilBusiness : BaseBusiness
    {
        public static UsuarioPerfilEntity Create(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.UsuarioPerfilEntity.FirstOrDefault(a => a.UsuarioPerfilId == id);
            }
        }

        public static List<UsuarioPerfilEntity> Pesquisar(string descricao, string sortBy, int startIndex, int pageSize, out int count)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = from up in context.UsuarioPerfilEntity
                            where up.Descricao.Contains(descricao)
                            select up;

                return dados.OrderBy(sortBy).Page(startIndex, pageSize, out count);
            }
        }

        public static List<UsuarioPerfilEntity> PesquisarDescricao(string descricao)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = (from usuarioPerfil in context.UsuarioPerfilEntity
                             where usuarioPerfil.Descricao.Contains(descricao)
                             orderby usuarioPerfil.Descricao
                             select usuarioPerfil).ToList();

                return dados;
            }
        }

        public void Salvar(UsuarioPerfilEntity entity, int usuarioId)
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

        protected override bool ConsisteInsertOrUpdate(IEntity entity, DbContext context)
        {
            var ticketContext = (SynsTicketContext)context;
            var usuarioPerfilEntity = (UsuarioPerfilEntity)entity;

            var descricaoDuplicada = from a in ticketContext.UsuarioPerfilEntity
                                     where a.UsuarioPerfilId != usuarioPerfilEntity.UsuarioPerfilId
                                            && a.Descricao.Equals(usuarioPerfilEntity.Descricao)
                                     select a;

            if (descricaoDuplicada.Count() > 0)
                this.Erros.Add(new TicketException("Campo descrição está duplicada"));

            return base.ConsisteInsertOrUpdate(entity, context);
        }

        public void Excluir(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                base.Remove<UsuarioPerfilEntity>(context, id);
            }
        }


    }
}
