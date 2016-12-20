using Syns.Ticket.Business;
using Syns.Ticket.Business.Entities;
using Syns.Ticket.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Controllers
{
    public class TicketAnexoController : BaseController
    {
         
           
        // EXEMPLO 2

        [HttpPost]
        public ActionResult Upload(TicketAnexoEntity model)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                string fileName = "";
                    
                string path = Server.MapPath("~/Arquivos/Ticket/Anexo/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                    
                if (file != null && file.ContentLength > 0)
                {

                    fileName = Path.GetFileNameWithoutExtension(file.FileName) + "-" + model.TicketId + Path.GetExtension(file.FileName);
                    
                    path = Path.Combine(path, fileName);

                    if (System.IO.File.Exists(path))
                        path = GetFileName(path);

                    file.SaveAs(path);
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        model.CaminhoArquivo = String.Format("~/Arquivos/Ticket/Anexo/{0}", fileName);
                        model.Visibilidade = 1;
                        new TicketAnexoBusiness().Salvar(model, this.UsuarioId);
                        return RedirectToAction("Ticket/Index");
                    }
                    catch (TicketException ex)
                    {
                        ModelState.AddModelError("ex", ex.Message);
                    }
                }
                            
            }

            return View("~/Views/Ticket/_Ticket.cshtml", model.TicketId);

           // return RedirectToAction("TicketAnexo");
        }

        private string GetFileName(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            string caminho = Path.GetDirectoryName(path);
            string extension = Path.GetExtension(path);

            fileName = fileName + new Random(999).Next();

            path = caminho + @"\" + fileName + extension;

            if (System.IO.File.Exists(path))
                return GetFileName(path);

            return path;
        }



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inserir()
        {
            return View("");
        }

        
    }
}
