using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Helpers
{
    public static class VisibilidadeDropdownHelpers
    {

        public static IEnumerable<SelectListItem> VisibilidadeDropdown(this HtmlHelper target, int? value = 0)
        {
            if (!value.HasValue)
                value = 0;

            var categoria = new List<SelectListItem>();

            foreach (var item in Visibilidade.Pesquisar())
            {
                categoria.Add(new SelectListItem() { Text = item.Descricao, Value = item.Id.ToString() });
            }

            return new SelectList(categoria, "Value", "Text", value.Value.ToString());
        }
    }



    public class Visibilidade
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public static List<Visibilidade> Pesquisar()
        {
            List<Visibilidade> dados = new List<Visibilidade>();


            dados.Add(new Visibilidade() { Descricao = "Interno", Id = 1 });
            dados.Add(new Visibilidade() { Descricao = "Externo", Id = 2 });
            dados.Add(new Visibilidade() { Descricao = "Publico", Id = 3 });
            dados.Add(new Visibilidade() { Descricao = "Critica", Id = 4 });

            return dados;
        }
    }
}