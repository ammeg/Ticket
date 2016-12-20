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
    public class UsuarioBusiness : BaseBusiness
    {

        public static List<UsuarioEntity> PesquisarTodos()
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.UsuarioEntity.ToList();
            }
        }

       
        public static UsuarioEntity Create(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.UsuarioEntity.FirstOrDefault(a => a.UsuarioId == id);
            }
        }
        public static List<UsuarioPesquisa> Pesquisar(string nome, string sortBy, int startIndex, int pageSize, out int count)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = PesquisarQuery(nome, context);

                return dados.OrderBy(sortBy).Page(startIndex, pageSize, out count);
            }
        }

        public static List<UsuarioPesquisa> Pesquisar(string nome)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = PesquisarQuery(nome, context);

                return dados.ToList();
            }
        }

        private static IQueryable<UsuarioPesquisa> PesquisarQuery(string nome, SynsTicketContext context)
        {
            var dados = from usuario in context.UsuarioEntity
                        join usuarioPerfil in context.UsuarioPerfilEntity on usuario.UsuarioPerfilId equals usuarioPerfil.UsuarioPerfilId
                        orderby usuario.Login
                        select new UsuarioPesquisa()
                        {
                            UsuarioId = usuario.UsuarioId,
                            Nome = usuario.Nome,
                            Login = usuario.Login,
                            UsuarioPerfil = usuarioPerfil.Descricao,
                        };

            if (!string.IsNullOrWhiteSpace(nome))
            {
                dados = dados.Where(a => a.Nome.Contains(nome));
            }
            return dados;
        }

        protected override bool ConsisteInsertOrUpdate(Entities.IEntity entity, DbContext context)
        {
            var ticketContext = (SynsTicketContext)context;
            var usuarioEntity = (UsuarioEntity)entity;

            var usarioLoginIguais = from a in ticketContext.UsuarioEntity
                                    where a.UsuarioId != usuarioEntity.UsuarioId
                                           && a.Login.Equals(usuarioEntity.Login)
                                    select a;

            var usarioEmailsIguais = from a in ticketContext.UsuarioEntity
                                    where a.UsuarioId != usuarioEntity.UsuarioId
                                           && a.Email.Equals(usuarioEntity.Email)
                                    select a;

            if (usarioLoginIguais.Count() > 0)
                this.Erros.Add(new TicketException("Campo Login está duplicado"));
            if (usarioEmailsIguais.Count() > 0)
                this.Erros.Add(new TicketException("Campo E-mail já Existe"));

            return base.ConsisteInsertOrUpdate(entity, context);
        }

        public void Salvar(UsuarioEntity entity, int usuarioId)
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
                base.Remove<UsuarioEntity>(context, id);
            }
        }


        //public void Login(UsuarioLoginModel entity)
        //{

        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var usuario = context.UsuarioEntity.FirstOrDefault(u => u.Login == model.Login && u.Senha == model.Senha);
        //        if (usuario != null)
        //        {
        //            FormsAuthentication.SetAuthCookie(model.Login, false);
        //            return RedirectToAction("Index");
        //        }
        //        return View(model);
        //    }

          
    }
}

