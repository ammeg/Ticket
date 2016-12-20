using Syns.Ticket.Business.Entities;
using Syns.Ticket.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syns.Ticket.ClientMvc.Models
{
    public class TicketEntityModel
    {
        public TicketEntity TicketEntity { get; set; }

        public List<TicketEntity> TicketListEntity { get; set; }

        public List<TicketPesquisa> TicketList { get; set; }
    }
}