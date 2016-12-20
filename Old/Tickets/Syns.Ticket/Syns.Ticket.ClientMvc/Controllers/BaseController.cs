using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Controllers
{
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
    public class BaseController : Controller
    {
        public int UsuarioId { get { return 1; } }
    }
}
