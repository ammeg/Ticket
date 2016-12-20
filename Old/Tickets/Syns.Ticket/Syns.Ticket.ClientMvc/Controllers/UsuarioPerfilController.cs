using Syns.Ticket.Business;
using Syns.Ticket.Business.Entities;
using System;
using Syns.Ticket.ClientMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syns.Ticket.Lib;

namespace Syns.Ticket.ClientMvc.Controllers
{
    public class UsuarioPerfilController : BaseController
    {
        //
        // GET: /Permissao/

        public ActionResult Index()
        {
            string descricao = "";

            if (descricao == "" && Request.Cookies["UsuarioPerfil.Descricao"] != null)
                descricao = Request.Cookies["UsuarioPerfil.Descricao"].Value;

            ViewBag.Descricao = descricao;

            return View();
        }
        public ActionResult Selecionar(int id)
        {
            var entity = UsuarioPerfilBusiness.Create(id);
            //Tem que passar no primeiro parâmetro a View que usamos para fazer o cadastro, se passar a Index, ele irá renderizar a View Index, que espera um model do tipo List.
            //return View("Index",entity);
            return View(entity);


        }

        public ActionResult Pesquisar()
        {
            DataTableHelper dataTable = new DataTableHelper();

            string descricao = Request.Params["descricao"].ToString();
            Response.Cookies["UsuarioPerfil.Descricao"].Value = descricao;

            int count = 0;

            var dados = UsuarioPerfilBusiness.Pesquisar(descricao, dataTable.sortBy, dataTable.startExibir, dataTable.regExibir, out count);

            var objeto = new
            {
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                sEcho = dataTable.echo,
                aaData = dados
            };

            return Json(objeto, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Inserir(UsuarioPerfilEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new UsuarioPerfilBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Editar(int id)
        {
            var entity = UsuarioPerfilBusiness.Create(id);

            return View(entity);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioPerfilEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new UsuarioPerfilBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Excluir(int id)
        {
            new UsuarioPerfilBusiness().Excluir(id);

            return RedirectToAction("Index");
        }


        public ActionResult UsuarioPerfilDescricaoUrl(string descricao)
        {
            var dados = UsuarioPerfilBusiness.PesquisarDescricao(descricao);
            return Json(dados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UsuarioPerfilIdUrl(int usuarioPerfilId)
        {
            var entity = UsuarioPerfilBusiness.Create(usuarioPerfilId);

            return Json(entity, JsonRequestBehavior.AllowGet);
        }
    }
}




