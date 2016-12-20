using Syns.Ticket.Business;
using Syns.Ticket.Business.Entities;
using Syns.Ticket.Business.Models;
using Syns.Ticket.ClientMvc.Models;
using Syns.Ticket.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Syns.Ticket.ClientMvc.Controllers
{
    public class TicketController : BaseController
    {

        public ActionResult Index()
        {
            //TicketModel model = new TicketModel();
            //model.TicketEntity = new TicketEntity();
            //model.TicketList = TicketBusiness.PesquisarTodosTicket(1).ToList();
            //return View("Index", model);


            string prioridade = "";
            if (prioridade == "" && Request.Cookies["Ticket.Prioridade"] != null)
                prioridade = Request.Cookies["Ticket.Prioridade"].Value;
            ViewBag.Prioridade = prioridade;

            string assunto = "";
            if (assunto == "" && Request.Cookies["Ticket.Assunto"] != null)
                assunto = Request.Cookies["Ticket.Assunto"].Value;
            ViewBag.Assunto = assunto;

            string departamento = "";
            if (departamento == "" && Request.Cookies["Ticket.Departamento"] != null)
                departamento = Request.Cookies["Ticket.Departamento"].Value;
            ViewBag.Departamento = prioridade;

            string usuario = "";
            if (usuario == "" && Request.Cookies["Ticket.Usuario"] != null)
                usuario = Request.Cookies["Ticket.Usuario"].Value;
            ViewBag.Usuario = usuario;

            string usuarioResponsavel = "";
            if (usuarioResponsavel == "" && Request.Cookies["Ticket.UsuarioResponsavel"] != null)
                usuarioResponsavel = Request.Cookies["Ticket.UsuarioResponsavel"].Value;
            ViewBag.UsuarioResponsavel = usuarioResponsavel;

            string categoria = "";
            if (categoria == "" && Request.Cookies["Ticket.Categoria"] != null)
                categoria = Request.Cookies["Ticket.Categoria"].Value;
            ViewBag.Categoria = categoria;

            return View();

        }
        public ActionResult Selecionar(int id)
        {
            var entity = TicketBusiness.Create(id);
            //Tem que passar no primeiro parâmetro a View que usamos para fazer o cadastro, se passar a Index, ele irá renderizar a View Index, que espera um model do tipo List.
            //return View("Index",entity);
            return View(entity);


        }

        public static List<TicketPesquisa> PesquisarTicket2(string categoria, string assunto, string departamento, string usuario, string usuarioResponsavel, string prioridade, string sortBy, int startIndex, int pageSize, out int count)
        {

            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = from ticket in context.TicketEntity
                            join usuario1 in context.UsuarioEntity on ticket.UsuarioId equals usuario1.UsuarioId
                            
                            join usuario2 in context.UsuarioEntity on ticket.UsuarioResponsavelId equals usuario2.UsuarioId into usuario2Left
                            from usuario2 in usuario2Left.DefaultIfEmpty()
                            
                            join departamento1 in context.DepartamentoEntity on ticket.DepartamentoId equals departamento1.DepartamentoId
                            orderby ticket.DataCadastro
                            select new TicketPesquisa()
                            {
                                TicketId = ticket.TicketId,
                                Assunto = ticket.Assunto,
                                DepartamentoId = ticket.DepartamentoId,
                                UsuarioResponsavel = ticket.UsuarioResponsavelId,
                                UsuarioId = ticket.UsuarioId,
                                Prioridade = ticket.Prioridade,
                                Categoria = ticket.CategoriaId,
                                Usuario = ticket.UsuarioId,
                                departamento = departamento1.Descricao,
                                usuario = usuario1.Nome,
                                usuarioResponsavel = usuario2.Nome,

                                Ticket2Id = ticket.TicketId,
                                Ticket3Id = ticket.TicketId,
                                Ticket4Id = ticket.TicketId,

                            };

                if (!string.IsNullOrWhiteSpace(categoria))
                {
                    dados = dados.Where(a => a.Categoria.Equals(categoria));
                }

                if (!string.IsNullOrWhiteSpace(assunto))
                {
                    dados = dados.Where(a => a.Assunto.Contains(assunto));
                }

                if (!string.IsNullOrWhiteSpace(prioridade))
                {
                    dados = dados.Where(a => a.Prioridade.Equals(prioridade));
                }

                if (!string.IsNullOrWhiteSpace(assunto))
                {
                    dados = dados.Where(a => a.Assunto.Contains(assunto));
                }

                if (!string.IsNullOrWhiteSpace(departamento))
                {
                    dados = dados.Where(a => a.Departamento.Contains(departamento));
                }


                return dados.OrderBy(sortBy).Page(startIndex, pageSize, out count);
            }
        }

        public ActionResult PesquisarTicketTable()
        {
            DataTableHelper dataTable = new DataTableHelper();

            string categoria = Request.Params["categoria"].ToString();
            Response.Cookies["Ticket.Categoria"].Value = categoria;

            string assunto = Request.Params["assunto"].ToString();
            Response.Cookies["Ticket.Assunto"].Value = assunto;


            string prioridade = Request.Params["prioridade"].ToString();
            Response.Cookies["Ticket.Prioridade"].Value = prioridade;

            string departamento = Request.Params["departamento"].ToString();
            Response.Cookies["Ticket.Departamento"].Value = departamento;

            string usuario = Request.Params["usuario"].ToString();
            Response.Cookies["Ticket.Usuario"].Value = usuario;

            string usuarioResponsavel = Request.Params["usuarioResponsavel"].ToString();
            Response.Cookies["Ticket.UsuarioResponsavel"].Value = usuarioResponsavel;

            int count = 0;

            var dados = PesquisarTicket2(categoria, assunto, departamento, usuario, usuarioResponsavel, prioridade, dataTable.sortBy, dataTable.startExibir, dataTable.regExibir, out count);

            var objeto = new
            {
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                sEcho = dataTable.echo,
                aaData = dados
            };


            return Json(objeto, JsonRequestBehavior.AllowGet);
        }
        /////////////////////////////// TICKET ///////////////////////////////////////////
        /////////////////                TICKET                ///////////////////////////
        //////////////////////////////////////////////////////////////////////////////////

        public ActionResult InserirTicket()
        {
            var model = new TicketModel();
            model.TicketEntity = new TicketEntity();
            model.TicketEntity.UsuarioId = this.UsuarioId;
            model.TicketEntity.DataCadastro = DateTime.Now;


            return View("PrincipalTicket", model);
        }

        [HttpPost]
        public ActionResult InserirTicket(TicketEntity entity)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    new TicketBusiness().Salvar(entity, this.UsuarioId);
                    return RedirectToAction("EditarTicket", new { @id = entity.TicketId });
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            TicketModel model = new TicketModel();
            model.TicketEntity = entity;
            return View("PrincipalTicket", model);
        }

        public ActionResult EditarTicket(int id)
        {
            var entity = TicketBusiness.Create(id);

            TicketModel model = new TicketModel();
            model.TicketEntity = new TicketEntity();
            model.TicketEntity = entity;

            model.TicketAnexoModel = new TicketAnexoModel();
            model.TicketAnexoModel.TicketAnexoEntity = new TicketAnexoEntity() { TicketId = entity.TicketId };
            model.TicketAnexoModel.TicketAnexoList = TicketBusiness.PesquisarAnexoPorTicket(entity.TicketId).ToList();

            model.TicketAndamentoModel = new TicketAndamentoModel();
            model.TicketAndamentoModel.TicketAndamentoEntity = new TicketAndamentoEntity() { TicketId = entity.TicketId };
            model.TicketAndamentoModel.TicketAndamentoList = TicketBusiness.PesquisarAndamentoPorTicket(entity.TicketId, 1).ToList();

            return View("PrincipalTicket", model);
        }

        [HttpPost]
        public ActionResult EditarTicket(TicketEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new TicketBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View("PrincipalTicket", model);
        }

        /// <summary>
        /// ANEXO
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>


        public ActionResult CancelarTicket(int id)
        {
            return RedirectToAction("Index");
        }

        /////////////////////////////////////////////////////////////////////////
        //////////////             TICKET ANDAMENTO        //////////////////////
        /////////////////////////////////////////////////////////////////////////

        public ActionResult InserirAndamento()
        {
            TicketAndamentoEntity entity = new TicketAndamentoEntity()
            {
                DataHora = DateTime.Now,
                Data = DateTime.Now,

            };
            return View("Index");

        }

        [HttpPost]
        public ActionResult InserirAndamento(TicketAndamentoEntity model)
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
            return Json(model, JsonRequestBehavior.AllowGet);
            //   return View("~/Views/Ticket/_Andamento.cshtml", model);

        }

        public ActionResult PesquisarAndamento()
        {

            DataTableHelper dataTable = new DataTableHelper();

            int ticketId = Convert.ToInt32(Request.Params["ticketId"].ToString());

            var dados = TicketBusiness.PesquisarAndamentoPorTicket(ticketId, 1);

            int count = 0;

            count = dados.Count();

            var lista = dados.Skip(dataTable.startExibir).Take(dataTable.regExibir).ToList();

            var objeto = new
            {
                iTotalRecords = count,
                iTotalDisplayRecords = count,

                aaData = lista
            };

            return Json(objeto, JsonRequestBehavior.AllowGet);


        }

        public ActionResult EditarAndamento(int id)
        {
            var entity = TicketAndamentoBusiness.Create(id);

            return Json(id, JsonRequestBehavior.AllowGet);
            //  return View("~/Views/Ticket/_Andamento.cshtml");
        }

        [HttpPost]
        public ActionResult EditarAndamento(TicketAndamentoEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new TicketAndamentoBusiness().Salvar(model, this.UsuarioId);

                    //return RedirectToAction("~/Views/Ticket/_Andamento.cshtml", model);
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
            //    return View("Index", model);
        }

        //////////////////////////////////////////////
        //////////////   TICKET ANEXO   //////////////
        //////////////////////////////////////////////

        public ActionResult _TicketAnexo()
        {

            var model = new TicketModel();
            model.TicketEntity = new TicketEntity();
            model.TicketAnexoModel.TicketAnexoEntity.TicketId = model.TicketEntity.TicketId;
            return View(TicketBusiness.PesquisarAnexoPorTicket(model.TicketEntity.TicketId).ToList());
        }


        [HttpPost]
        public ActionResult InserirTicketAnexo(TicketAnexoEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new TicketAnexoBusiness().Salvar(model, this.UsuarioId);
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            return View("PrincipalTicket", model.TicketId);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarArquivo(IEnumerable<HttpPostedFileBase> files, TicketAnexoEntity entity)
        {
            TicketModel model2 = new TicketModel();
            TicketAnexoModel model = new TicketAnexoModel();
            model.TicketAnexoEntity = entity;

            if (files != null)
            {
                foreach (var file in files)
                {
                    string fileName = "";

                    string path = Server.MapPath("~/Arquivos/Ticket/Anexo/");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        // extract only the fielname
                        fileName = Path.GetFileNameWithoutExtension(file.FileName) + "-" + entity.TicketId + Path.GetExtension(file.FileName);

                        path = Path.Combine(path, fileName);

                        if (System.IO.File.Exists(path))
                        {
                            path = GetFileName(path);
                            fileName = Path.GetFileName(path);

                        }
                        file.SaveAs(path);
                        if (ModelState.IsValid)
                        {
                            var entity2 = new TicketAnexoEntity();
                            entity2.TicketId = entity.TicketId;
                            entity2.Observacao = entity.Observacao;
                            entity2.CaminhoArquivo = "~/Arquivos/Ticket/Anexo/" + fileName;
                            // entity2.TpArq = Path.GetExtension(fileName);
                            new TicketAnexoBusiness().Salvar(entity2, this.UsuarioId);
                        }

                    }
                }
            }

            model.TicketAnexoEntity = new TicketAnexoEntity()
            {
                TicketId = entity.TicketId
            };
            //context.TicketAnexoEntity.Where(a => a.TicketId == entity.TicketId)
            model.TicketAnexoList = TicketBusiness.PesquisarAnexoPorTicket(entity.TicketId).ToList();

            return PartialView("_TicketAnexo", model);
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






    }
}
