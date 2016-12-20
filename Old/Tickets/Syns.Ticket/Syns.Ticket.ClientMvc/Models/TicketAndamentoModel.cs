using Syns.Ticket.Business.Entities;
using Syns.Ticket.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syns.Ticket.ClientMvc.Models
{
    public class TicketAndamentoModel
    {
        public TicketAndamentoEntity TicketAndamentoEntity { get; set; }

        public List<TicketAndamentoEntity> TicketAndamentoEntityList { get; set; }

        public List<TicketAndamentoPesquisa> TicketAndamentoList { get; set; }
    }
}