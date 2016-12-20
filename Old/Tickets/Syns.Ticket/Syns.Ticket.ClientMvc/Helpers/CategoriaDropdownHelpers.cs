using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Helpers
{
    public static class CategoriaDropdownHelpers
    {
        public static IEnumerable<SelectListItem> CategoriaDropDown(this HtmlHelper target, int? value = 0)
        {
            if (!value.HasValue)
                value = 0;

            var categoria = new List<SelectListItem>();

            foreach (var item in Categoria.Pesquisar())
            {
                categoria.Add(new SelectListItem() { Text = item.Descricao, Value = item.Id.ToString() });
            }

            return new SelectList(categoria, "Value", "Text", value.Value.ToString());
        }
    }



    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public static List<Categoria> Pesquisar()
        {
            List<Categoria> dados = new List<Categoria>();


            dados.Add(new Categoria() { Descricao = "Erro", Id = 1 });
            dados.Add(new Categoria() { Descricao = "Dúvida", Id = 2 });
            dados.Add(new Categoria() { Descricao = "Sugestão", Id = 3 });
            dados.Add(new Categoria() { Descricao = "Critica", Id = 4 });

            return dados;
        }
    }
}
