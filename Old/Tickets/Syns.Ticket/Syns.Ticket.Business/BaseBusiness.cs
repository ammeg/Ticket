using Syns.Ticket.Business.Entities;
using Syns.Ticket.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business
{
    public class BaseBusiness
    {
        private List<TicketException> _erros;
        protected List<TicketException> Erros
        {
            get
            {
                if (_erros == null)
                    _erros = new List<TicketException>();

                return _erros;
            }
            set
            {
                _erros = value;
            }
        }

        protected string ConsisteErros()
        {
            string mensagen = "";
            if (this.Erros.Count > 0)
            {
                foreach (var erro in Erros)
                {

                    mensagen += erro.Message;
                    if (!mensagen.EndsWith("\n"))
                        mensagen += "\n";
                }
                Erros.Clear();
            }
            return mensagen;
        }        

        protected virtual bool ConsisteInsertOrUpdate(IEntity entity, DbContext context)
        {
            var erros = context.GetValidationErrors();

            foreach (var erro in erros)
            {
                foreach (var detalheErro in erro.ValidationErrors)
                {
                    this.Erros.Add(new TicketException(detalheErro.ErrorMessage));
                }
            }

            var entityValidation = context.Entry(entity).GetValidationResult();

            foreach (var erro in entityValidation.ValidationErrors)
            {
                this.Erros.Add(new TicketException(erro.ErrorMessage));
            }

            return this.Erros.Count == 0;
        }

        private object[] GetKeyValue<T>(T entity) where T : class, IEntity
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var keysProperties = properties.Where(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);

            //if (keysProperties.Count() > 1)            
            //    throw new InvalidOperationException("O método GetKeyValue não pode ser usado para classes com mais de uma propriedade Key.");

            //PropertyInfo key = keysProperties.First();

            List<object> retorno = new List<object>();

            foreach (var key in keysProperties)
            {
                retorno.Add(key.GetValue(entity, null));
            }

            return retorno.ToArray(); // key != null ? key.GetValue(entity, null) : null;
        }
        
        protected bool SaveChanges<T>(T entity, int usuarioId, bool save, DbContext context) where T : class, IEntity
        {
            try
            {

                if (this.ConsisteInsertOrUpdate(entity, context))
                {
                    Save<T>(entity, usuarioId, save, context);
                }
            }
            finally
            {
                if (this.Erros.Count > 0)
                    throw new TicketException(this.ConsisteErros());
            }

            return true;
        }

        protected bool Save<T>(T entity, int usuarioId, bool save, DbContext context) where T : class, IEntity
        {
            try
            {
                entity.DataHoraCad = DateTime.Now;
                entity.UsuarioCadId = usuarioId;

                if (entity == null)
                {
                    throw new ArgumentException("Cannot add a null entity.");
                }


                var entry = context.Entry<T>(entity);

                if (entry.State == EntityState.Detached)
                {
                    var set = context.Set<T>();
                    T attachedEntity = set.Find(this.GetKeyValue(entity));  // You need to have access to key

                    if (attachedEntity != null)
                    {
                        var attachedEntry = context.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                        attachedEntry.OriginalValues.SetValues(attachedEntity);
                    }
                    else
                    {
                        context.Set<T>().Add(entity);
                    }
                }

                if (save)
                    context.SaveChanges();

            }
            catch (DbEntityValidationException)
            {
                string msgErro = string.Empty;
                var erros = context.GetValidationErrors();
                foreach (var erro in erros)
                {
                    foreach (var detalheErro in erro.ValidationErrors)
                    {
                        msgErro += detalheErro.ErrorMessage + "\n";
                    }
                }
                if (context.Entry((object)entity).State == EntityState.Added)
                    context.Entry((object)entity).State = EntityState.Detached;

                throw new InvalidOperationException(msgErro);
            }
            finally
            {
                if (this.Erros.Count > 0)
                    throw new TicketException(this.ConsisteErros());
            }
            return true;
        }

        protected bool SaveChanges<T>(T entity, int usuarioId, DbContext context) where T : class, IEntity
        {
            return this.SaveChanges<T>(entity, usuarioId, true, context);
        }

        protected bool Remove<T>(DbContext context, params object[] keyValues) where T : class, IEntity
        {
            var entity = this.Find<T>(context, keyValues);

            if (entity == null)
                throw new TicketException("Nenhum registro excluído");
            else
            {
                DbSet dbSet = context.Set<T>();
                //dbSet.Attach(entity);
                dbSet.Remove(entity);
                int saved = context.SaveChanges();

                return true;
            }
        }

        protected T Find<T>(DbContext context, params object[] keyValues) where T : class, IEntity
        {
            return context.Set<T>().Find(keyValues);
        }
    }
}
