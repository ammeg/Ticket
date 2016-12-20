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
    public class UsuarioDepartamentoBusiness : BaseBusiness
    {
        public static UsuarioDepartamentoEntity Create(int usuarioId, int departamentoId)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return context.UsuarioDepartamentoEntity.FirstOrDefault(a => a.UsuarioId == usuarioId && a.DepartamentoId == departamentoId);
            }
        }

        //public static List<UsuarioDepartamentoEntity> Pesquisar()
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        return context.UsuarioDepartamentoEntity.ToList();
        //    }
        //}

        public static List<UsuarioDepartamentoPesquisa> Pesquisar(string usuarioNome, string departamentoDescricao, string sortBy, int startIndex, int pageSize, out int count)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = from ud in context.UsuarioDepartamentoEntity
                            join usuario in context.UsuarioEntity on ud.UsuarioId equals usuario.UsuarioId
                            join departamento in context.DepartamentoEntity on ud.DepartamentoId equals departamento.DepartamentoId
                            orderby departamento.DepartamentoId
                            select new UsuarioDepartamentoPesquisa()
                            {
                                UsuarioId = usuario.UsuarioId,
                                DepartamentoId = departamento.DepartamentoId,
                                Usuario = usuario.Nome,
                                Departamento = departamento.Descricao,
                                Responsavel = ud.Responsavel
                            };

                if (!string.IsNullOrWhiteSpace(usuarioNome))
                {
                    if (usuarioNome == null)
                        usuarioNome = "";

                    var temp = usuarioNome.Split(' ');

                    dados = dados.Where(a => temp.All(t => a.Usuario.Contains(t)));
                }

                if (!String.IsNullOrWhiteSpace(departamentoDescricao))
                    dados = dados.Where(a => a.Departamento.Contains(departamentoDescricao));

                return dados.OrderBy(sortBy).Page(startIndex, pageSize, out count);
            }
        }


        //usuario cadastro
        public void Salvar(UsuarioDepartamentoEntity entity, int usuarioId)
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
            var usuarioDepartamento = (UsuarioDepartamentoEntity)entity;

            var descricaoDuplicada = from a in ticketContext.UsuarioDepartamentoEntity
                                     where a.DepartamentoId != usuarioDepartamento.DepartamentoId
                                            && a.DepartamentoId.Equals(usuarioDepartamento.DepartamentoId)
                                            && a.UsuarioId.Equals(usuarioDepartamento.UsuarioId)
                                     select a;

            if (descricaoDuplicada.Count() > 0)
                this.Erros.Add(new TicketException("O usuário já está cadastrado neste Departamento"));

            return base.ConsisteInsertOrUpdate(entity, context);
        }

        public void Excluir(int usuarioId, int departamentoId)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                base.Remove<UsuarioDepartamentoEntity>(context, usuarioId, departamentoId);
            }
        }

    }
}
