using Syns.Ticket.Business.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Syns.Ticket.Business
{
    public class SynsTicketContext : DbContext
    {
        public DbSet<UsuarioEntity> UsuarioEntity { get; set; }

        public DbSet<SistemaEntity> SistemaEntity { get; set; }

        public DbSet<UsuarioPerfilEntity> UsuarioPerfilEntity { get; set; }

        public DbSet<DepartamentoEntity> DepartamentoEntity { get; set; }

        public DbSet<TicketEntity> TicketEntity { get; set; }

        public DbSet<UsuarioDepartamentoEntity> UsuarioDepartamentoEntity { get; set; }

        public DbSet<TicketAnexoEntity> TicketAnexoEntity { get; set; }

        public DbSet<TicketAndamentoEntity> TicketAndamentoEntity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            Database.SetInitializer<SynsTicketContext>(new CreateDatabaseIfNotExists<SynsTicketContext>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
