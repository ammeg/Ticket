using Syns.Ticket.Business.Entities;
using Syns.Ticket.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syns.Ticket.ClientMvc.Models
{
    public class TicketAnexoModel
    {
        public TicketAnexoEntity TicketAnexoEntity { get; set; }

        public List<TicketAnexoPesquisa> TicketAnexoList { get; set; }
    }
}