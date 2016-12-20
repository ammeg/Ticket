using Syns.Ticket.Business;
using Syns.Ticket.Business.Entities;
using Syns.Ticket.ClientMvc.Models;
using Syns.Ticket.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Syns.Ticket.ClientMvc.Controllers
{
    public class DepartamentoController : BaseController
    {
        //código antigo
        //
        // GET: /Departamento/

        //public ActionResult Index()
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        return View(context.DepartamentoEntity.ToList());
        //    }
        //}

        //public ActionResult GetDepartamento(string query)
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var dados = context.DepartamentoEntity.Where(v => v.Descricao.Contains(query)).ToList();

        //        return Json(dados, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //#region Autocomplete de departamento
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="descricao"></param>
        ///// <returns></returns>


        //#endregion

        ////   ********** NÃO SERÁ MAIS UTILIZADO ESTE AUTO COMPLETE **********
        ////public ActionResult GetDepartamento(string query)
        ////{
        ////    using (SynsTicketContext context = new SynsTicketContext())
        ////    {
        ////        var dados = context.DepartamentoEntity.Where(v => v.Descricao.Contains(query)).ToList();

        ////        return Json(dados, JsonRequestBehavior.AllowGet);
        ////    }
        ////}



        //public ActionResult Inserir()
        //{
        //    return View("Departamento");
        //}


        //[HttpPost]
        //public ActionResult Inserir(DepartamentoEntity model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (SynsTicketContext context = new SynsTicketContext())
        //        {
        //            model.UsuarioCadId = 1;
        //            model.DataHoraCad = DateTime.Now;
        //            model.Ativo = true;
        //            context.DepartamentoEntity.Add(model);
        //            context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View("Departamento", model);
        //}

        //public ActionResult Editar(int id)
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var departamento = context.DepartamentoEntity.FirstOrDefault(u => u.DepartamentoId == id);

        //        return View("Departamento", departamento);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Editar(DepartamentoEntity model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (SynsTicketContext context = new SynsTicketContext())
        //        {
        //            var entry = context.Entry<DepartamentoEntity>(model);
        //            entry.State = System.Data.Entity.EntityState.Modified;
        //            entry.Entity.DataHoraCad = DateTime.Now;
        //            entry.Entity.UsuarioCadId = 1;
        //            entry.Entity.Ativo = true;
        //            context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View("Departamento", model);
        //}

        //public ActionResult Excluir(int id)
        //{
        //    using (SynsTicketContext context = new SynsTicketContext())
        //    {
        //        var entity = context.DepartamentoEntity.FirstOrDefault(a => a.DepartamentoId == id);
        //        context.DepartamentoEntity.Remove(entity);
        //        context.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //}
        public ActionResult Selecionar(int id)
        {
            var entity = DepartamentoBusiness.Create(id);
            //Tem que passar no primeiro parâmetro a View que usamos para fazer o cadastro, se passar a Index, ele irá renderizar a View Index, que espera um model do tipo List.
            //return View("Index",entity);
            return View(entity);


        }

        public ActionResult Index()
        {
            string descricao = "";

            if (descricao == "" && Request.Cookies["Departamento.Descricao"] != null)
                descricao = Request.Cookies["Departamento.Descricao"].Value;

            ViewBag.Descricao = descricao;

            return View();
        }
        //public ActionResult Index()
        //{
        //    return View(DepartamentoBusiness.Pesquisar(""));
        //}
        public ActionResult Pesquisar()
        {
            DataTableHelper dataTable = new DataTableHelper();

            string descricao = Request.Params["descricao"].ToString();
            Response.Cookies["Departamento.Descricao"].Value = descricao;

            int count = 0;

            var dados = DepartamentoBusiness.Pesquisar(descricao, dataTable.sortBy, dataTable.startExibir, dataTable.regExibir, out count);

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
            return View("Departamento");
        }

        [HttpPost]
        public ActionResult Inserir(DepartamentoEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new DepartamentoBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View("Departamento", model);
        }

        public ActionResult Editar(int id)
        {
            var entity = DepartamentoBusiness.Create(id);

            return View("Departamento", entity);
        }

        [HttpPost]
        public ActionResult Editar(DepartamentoEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new DepartamentoBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View("Departamento", model);
        }

        public ActionResult Excluir(int id)
        {
            new DepartamentoBusiness().Excluir(id);

            return RedirectToAction("Index");
        }

        #region Auto Complete Java Scrips
        /// <summary>
        /// Java Scrips
        /// </summary>
        /// <param name="JavaScripsAutoComplete"></param>
        /// <returns></returns>
        /// 
        public ActionResult DepartamentoDescricaoUrl(string descricao)
        {
            var dados = DepartamentoBusiness.PesquisarDescricao(descricao);
            return Json(dados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DepartamentoIdUrl(int departamentoId)
        {
            var entity = DepartamentoBusiness.Create(departamentoId);

            return Json(entity, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
