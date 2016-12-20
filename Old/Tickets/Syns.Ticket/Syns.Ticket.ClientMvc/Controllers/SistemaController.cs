using Syns.Ticket.Business;
using Syns.Ticket.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Controllers
{
    public class SistemaController : Controller
    {
        //
        // GET: /Sistema/

        public ActionResult Index()
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                return View(context.SistemaEntity.ToList());
            }
        }

        public ActionResult Excluir(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var entity = context.SistemaEntity.FirstOrDefault(a => a.SistemaId == id);
                context.SistemaEntity.Remove(entity);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Inserir()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Inserir(SistemaEntity model)
        {
            if (ModelState.IsValid)
            {
                using (SynsTicketContext context = new SynsTicketContext())
                {
                    model.UsuarioCadId = 1;
                    model.DataHoraCad = DateTime.Now;
                    model.Ativo = true;
                    context.SistemaEntity.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Editar(int id)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var sistema = context.SistemaEntity.FirstOrDefault(a => a.SistemaId == id);

                return View(sistema);
            }
        }

        [HttpPost]
        public ActionResult Editar(SistemaEntity model)
        {
            if (ModelState.IsValid)
            {
                using (SynsTicketContext context = new SynsTicketContext())
                {
                    var entry = context.Entry<SistemaEntity>(model);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Entity.DataHoraCad = DateTime.Now;
                    entry.Entity.UsuarioCadId = 1;
                    entry.Entity.Ativo = true;
                    context.SaveChanges();
                    return RedirectToAction("Index");                 
                }

            }

            return View(model);
        }

    }
}
