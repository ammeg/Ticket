using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticket.Models;

namespace Ticket.Data
{
    public class TicketContext : DbContext
    {
        //Models
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Sistema> Sistema { get; set; }

        public DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }

        public DbSet<Departamento> Departamento { get; set; }

        public DbSet<Tickets> Ticket { get; set; }

        public DbSet<UsuarioDepartamento> UsuarioDepartamento { get; set; }

        public DbSet<TicketAnexo> TicketAnexo { get; set; }

        public DbSet<TicketAndamento> TicketAndamento { get; set; }

        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
