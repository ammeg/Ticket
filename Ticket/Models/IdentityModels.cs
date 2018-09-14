using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Ticket.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            Auditar();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            Auditar();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void Auditar()
        {

            var states = new List<EntityState>() { EntityState.Added, EntityState.Deleted, EntityState.Modified };

            var entidades = ChangeTracker.Entries()
                                         .Where(e => e.Entity != null)
                                         .Where(e => states.Contains(e.State))
                                         .Where(e => typeof(IEntity).IsAssignableFrom(e.Entity.GetType()));

            foreach (var entry in entidades)
            {
                var currentTime = DateTime.Now;

                if (entry.Entity is IEntity)
                {
                    if (entry.Property(nameof(IEntity.DataHoraCad)) != null)
                    {
                        entry.Property(nameof(IEntity.DataHoraCad)).CurrentValue = currentTime;
                    }
                    if (entry.Property(nameof(IEntity.UsuarioCad)) != null && HttpContext.Current != null)
                    {
                        entry.Property(nameof(IEntity.UsuarioCad)).CurrentValue = HttpContext.Current?.User?.Identity?.GetUserName() ?? "";
                    }
                }
            }

        }

        public DbSet<Ticket.Models.Departamento> Departamentoes { get; set; }

        public DbSet<Ticket.Models.TicketAnexo> TicketAnexoes { get; set; }
    }
}