using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ticket.Models;

namespace Ticket.ViewModels
{ 
    public class TicketAnexosViewModel : PagedListViewModel<TicketAnexo>
    {
		//TODO: adicionar filtros de pesquisa
		public string Definir { get; set; }
	}
}
