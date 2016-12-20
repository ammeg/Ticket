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
    public class UsuarioDepartamentoController : BaseController
    {

        public ActionResult Selecionar(int usuario, int departamento)
        {
            var entity = UsuarioDepartamentoBusiness.Create(usuario, departamento);
            //Tem que passar no primeiro parâmetro a View que usamos para fazer o cadastro, se passar a Index, ele irá renderizar a View Index, que espera um model do tipo List.
            //return View("Index",entity);
            return View("Selecionar", entity);


        }
        //Código antigo
        // GET: /UsuarioDepartamento/

        //public ActionResult Index()
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        return View(context.UsuarioDepartamentoEntity.ToList());
        //    }
        //}

        //public ActionResult Inserir()
        //{
        //    return View("UsuarioDepartamento");
        //}

        //[HttpPost]
        //public ActionResult Inserir(UsuarioDepartamentoEntity model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (SynsTicketContext context = new SynsTicketContext())
        //        {
        //            model.UsuarioCadId = 1;
        //            model.DataHoraCad = DateTime.Now;
        //            model.Ativo = true;
        //            context.UsuarioDepartamentoEntity.Add(model);
        //            context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View("UsuarioDepartamento", model);
        //}

        ////aqui preciso fazer a edição dos campos, porém preciso que os dois parâmetros 
        //public ActionResult Editar(int usuarioId, int departamentoId)
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var usuarioDepartamento = context.UsuarioDepartamentoEntity.FirstOrDefault(a => a.UsuarioId == usuarioId && a.DepartamentoId == departamentoId);
        //        return View("UsuarioDepartamento", usuarioDepartamento);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Editar(UsuarioDepartamentoEntity model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (SynsTicketContext context = new SynsTicketContext())
        //        {
        //            var entry = context.Entry<UsuarioDepartamentoEntity>(model);
        //            entry.State = System.Data.Entity.EntityState.Modified;
        //            entry.Entity.DataHoraCad = DateTime.Now;
        //            entry.Entity.UsuarioCadId = 1;
        //            entry.Entity.Ativo = true;
        //            context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View("UsuarioDepartamento", model);
        //}

        //public ActionResult Excluir(int usuarioId, int departamentoId)
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var entity = context.UsuarioDepartamentoEntity.FirstOrDefault(a => a.UsuarioId == usuarioId);
        //        context.UsuarioDepartamentoEntity.Remove(entity);
        //        context.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //}

        public ActionResult Index()
        {
            string descricao = "";
            string nome = "";

            if (descricao == "" && Request.Cookies["UsuarioDepartamento.Descricao"] != null)
                descricao = Request.Cookies["UsuarioDepartamento.Descricao"].Value;

            if (nome == "" && Request.Cookies["UsuarioDepartamento.Nome"] != null)
                nome = Request.Cookies["UsuarioDepartamento.Nome"].Value;

            ViewBag.Descricao = descricao;
            ViewBag.Nome = nome;

            return View();
        }

        public ActionResult Pesquisar()
        {
            DataTableHelper dataTable = new DataTableHelper();

            string descricao = Request.Params["descricao"].ToString();
            Response.Cookies["UsuarioDepartamento.Descricao"].Value = descricao;

            string nome = Request.Params["nome"].ToString();
            Response.Cookies["UsuarioDepartamento.Nome"].Value = nome;

            int count = 0;

            var dados = UsuarioDepartamentoBusiness.Pesquisar(nome, descricao, dataTable.sortBy, dataTable.startExibir, dataTable.regExibir, out count);

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
            return View("UsuarioDepartamento");
        }

        [HttpPost]
        public ActionResult Inserir(UsuarioDepartamentoEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new UsuarioDepartamentoBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Editar(int usuarioId, int departamentoId)
        {
            var entity = UsuarioDepartamentoBusiness.Create(usuarioId, departamentoId);

            return View(entity);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioDepartamentoEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new UsuarioDepartamentoBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Excluir(int usuarioId, int departamentoId)
        {
            new UsuarioDepartamentoBusiness().Excluir(usuarioId, departamentoId);

            return RedirectToAction("Index");
        }

        #region JavaScrips Autocomplete Usuário e Departamento
        //////////////////////////////////////////////////////////////////////
        /////// ******** ---      DEPARTAMENTO   ----   *************////////
        ////////////////////////////////////////////////////////////////////
        public ActionResult DepartamentoDescricaoUrl(string descricao)
        {
            var dados = UsuarioPerfilBusiness.PesquisarDescricao(descricao);
            return Json(dados, JsonRequestBehavior.AllowGet);
        }

        //////////////////////////////////////////////////////////////////////
        /////// ******** ---      USUÁRIO   ----   *******************////////
        //////////////////////////////////////////////////////////////////////

        public ActionResult GetUsuario(int usuarioId)
        {
            var entity = UsuarioBusiness.Create(usuarioId);

            return Json(entity, JsonRequestBehavior.AllowGet);
        }

        #endregion



    }
}
