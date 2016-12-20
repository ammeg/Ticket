using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Syns.Ticket.ClientMvc.Helpers
{
    public static class TicketPrioridadeDropdownHelpers
    {
        public static IEnumerable<SelectListItem> PrioridadeDropDown(this HtmlHelper target, int? value = 0)
        {
            if (!value.HasValue)
                value = 0;

            var prioridade = new List<SelectListItem>();

            foreach (var item in Prioridade.Pesquisar())
            {
                prioridade.Add(new SelectListItem() { Text = item.Descricao, Value = item.Id.ToString() });
            }

            return new SelectList(prioridade, "Value", "Text", value.Value.ToString());
        }

    }


    public class Prioridade
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public static List<Prioridade> Pesquisar()
        {
            List<Prioridade> dados = new List<Prioridade>();


            dados.Add(new Prioridade() { Descricao = "Baixa", Id = 1 });
            dados.Add(new Prioridade() { Descricao = "Média", Id = 2 });
            dados.Add(new Prioridade() { Descricao = "Alta", Id = 3 });
            dados.Add(new Prioridade() { Descricao = "Muito Alta", Id = 4 });

            return dados;
        }
    }

    //public class Visibilidade
    //{
    //    public int Id { get; set; }
    //    public string Descricao { get; set; }
    //    public static List<Visibilidade> Pesquisar()
    //    {
    //        List<Visibilidade> dados = new List<Visibilidade>();


    //        dados.Add(new Visibilidade() { Descricao = "Privada", Id = 1 });
    //        dados.Add(new Visibilidade() { Descricao = "Interna", Id = 2 });
    //        dados.Add(new Visibilidade() { Descricao = "Externa", Id = 3 });

    //        return dados;
    //    }
    //}

    //public class Categoria
    //{
    //    public int Id { get; set; }
    //    public string Descricao { get; set; }
    //    public static List<Categoria> Pesquisar()
    //    {
    //        List<Categoria> dados = new List<Categoria>();


    //        dados.Add(new Categoria() { Descricao = "Erro", Id = 1 });
    //        dados.Add(new Categoria() { Descricao = "Dúvida", Id = 2 });
    //        dados.Add(new Categoria() { Descricao = "Sugestão", Id = 3 });
    //        dados.Add(new Categoria() { Descricao = "Sugestão", Id = 3 });
    //        dados.Add(new Categoria() { Descricao = "Sugestão", Id = 3 });

    //        return dados;
    //    }
    //}
}