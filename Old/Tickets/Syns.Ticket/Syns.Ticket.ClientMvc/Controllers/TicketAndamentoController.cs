using Syns.Ticket.Business;
using Syns.Ticket.Business.Entities;
using Syns.Ticket.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Controllers
{
    public class TicketAndamentoController : BaseController
    {
        //
        // GET: /TicketAndamento/

        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Index()
        //{
        //    return View(DepartamentoBusiness.Pesquisar(""));
        //}
        //public ActionResult Pesquisar()
        //{
        //    DataTableHelper dataTable = new DataTableHelper();

        //    string descricao = Request.Params["descricao"].ToString();
        //    Response.Cookies["Departamento.Descricao"].Value = descricao;

        //    int count = 0;

        //    var dados = TicketAndamentoBusiness.PesquisarNovo(descricao, dataTable.sortBy, dataTable.startExibir, dataTable.regExibir, out count);

        //    var objeto = new
        //    {
        //        iTotalRecords = count,
        //        iTotalDisplayRecords = count,
        //        sEcho = dataTable.echo,
        //        aaData = dados
        //    };

        //    return Json(objeto, JsonRequestBehavior.AllowGet);
        //}


        
        public ActionResult Inserir()
        {
            TicketAndamentoEntity entity = new TicketAndamentoEntity()
            {
                DataHora = DateTime.Now,
                Data = DateTime.Now,

            };
            return View("Index");

        }

        [HttpPost]
        public ActionResult Inserir(TicketAndamentoEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new TicketAndamentoBusiness().Salvar(model, this.UsuarioId);

                    // quando usado o ajax isto não existe
                    // return RedirectToAction("_PrincipalTicket", model);
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View("~/Views/Ticket/_Andamento.cshtml", model);

        }

        public ActionResult Editar(int id)
        {
            var entity = TicketAndamentoBusiness.Create(id);

            return View("~/Views/Ticket/_Andamento.cshtml", entity);
        }

        [HttpPost]
        public ActionResult Editar(TicketAndamentoEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new TicketAndamentoBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("~/Views/Ticket/_Andamento.cshtml");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View("Index", model);
        }

        public ActionResult Excluir(int id)
        {
            new TicketAndamentoBusiness().Excluir(id);

            return RedirectToAction("Index");
        }
    }


}
