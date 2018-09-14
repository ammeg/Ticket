using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ticket.Models;

namespace Ticket.ViewModels
{ 
    public class DepartamentosViewModel : PagedListViewModel<Departamento>
    {
		//TODO: adicionar filtros de pesquisa
		public string Descricao { get; set; }
	}
}
