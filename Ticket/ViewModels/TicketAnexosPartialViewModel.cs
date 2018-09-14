using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ticket.Models;

namespace Ticket.ViewModels
{ 
    public class TicketAnexosPartialViewModel
    {
		public int TicketId { get; set; }

        public TicketAnexosViewModel Pesquisa { get; set; }
		
	}
}
