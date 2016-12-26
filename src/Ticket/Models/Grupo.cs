using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Models
{
    //IdentityRole<TKey, TUserRole, IdentityRoleClaim<TKey>>
    public class Grupo : IdentityRole<Guid>
    {
    }
}
