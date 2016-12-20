using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Helpers
{
    public static class StatusDropdownHelpers
    {

        public static IEnumerable<SelectListItem> StatusDropDown(this HtmlHelper target, int? value = 0)
        {
            if (!value.HasValue)
                value = 0;

            var categoria = new List<SelectListItem>();

            foreach (var item in Status.Pesquisar())
            {
                categoria.Add(new SelectListItem() { Text = item.Descricao, Value = item.Id.ToString() });
            }

            return new SelectList(categoria, "Value", "Text", value.Value.ToString());
        }
    }



    public class Status
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public static List<Status> Pesquisar()
        {
            List<Status> dados = new List<Status>();


            dados.Add(new Status() { Descricao = "Aberto", Id = 1 });
            dados.Add(new Status() { Descricao = "Aguardando Resposta", Id = 2 });
            dados.Add(new Status() { Descricao = "Cancelado", Id = 5 });
            dados.Add(new Status() { Descricao = "Fechado", Id = 3 });
            dados.Add(new Status() { Descricao = "Outro", Id = 4 });

            return dados;
        }
    }
}