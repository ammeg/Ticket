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
   
    public class UsuarioController : BaseController
    {
        //
        // GET: /Usuario/


        public ActionResult Index()
        {
            string nome = "";

            if (nome == "" && Request.Cookies["Usuario.Nome"] != null)
                nome = Request.Cookies["Usuario.Nome"].Value;

            ViewBag.Nome = nome;

            return View();
        }

        //public ActionResult Index()
        //{
        //    return View(UsuarioBusiness.Pesquisar(""));
        //}

        public ActionResult GetUsuarioPerfil(string query)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var dados = context.UsuarioPerfilEntity.Where(v => v.Descricao.Contains(query)).ToList();

                return Json(dados, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Pesquisar()
        {
            DataTableHelper dataTable = new DataTableHelper();

            string nome = Request.Params["nome"].ToString();
            Response.Cookies["Usuario.Nome"].Value = nome;

            int count = 0;

            var dados = UsuarioBusiness.Pesquisar(nome, dataTable.sortBy, dataTable.startExibir, dataTable.regExibir, out count);

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
            return View("Usuario");
        }

        [HttpPost]
        public ActionResult Inserir(UsuarioEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new UsuarioBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index", "Usuario");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            //Caso o ModelState.IsValid seja false, tem que retornar para a View Usuario passando o Model, 
            //caso não seja informada a View, que é para renderizar, a Engine do Razor, 
            //tentará renderizar uma view com o mesmo nome do metódo
            //return View(model);
            return View("Usuario", model);
        }

        public ActionResult Editar(int id)
        {
            var entity = UsuarioBusiness.Create(id);
            //Tem que passar no primeiro parâmetro a View que usamos para fazer o cadastro, se passar a Index, ele irá renderizar a View Index, que espera um model do tipo List.
            //return View("Index",entity);
            return View("Usuario", entity);


        }

        public ActionResult Selecionar(int id)
        {
            var entity = UsuarioBusiness.Create(id);
            //Tem que passar no primeiro parâmetro a View que usamos para fazer o cadastro, se passar a Index, ele irá renderizar a View Index, que espera um model do tipo List.
            //return View("Index",entity);
            return View(entity);


        }

        [HttpPost]
        public ActionResult Editar(UsuarioEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    new UsuarioBusiness().Salvar(model, this.UsuarioId);
                    return RedirectToAction("Index", "Usuario");
                }
                catch (TicketException ex)
                {
                    ModelState.AddModelError("ex", ex.Message);
                }
            }
            //Caso o ModelState.IsValid seja false, tem que retornar para a View Usuario passando o Model, 
            //caso não seja informada a View, que é para renderizar, a Engine do Razor, 
            //tentará renderizar uma view com o mesmo nome do metódo
            //return View(model);
            return View("Usuario", model);
        }

        public ActionResult Excluir(int id)
        {
            new UsuarioBusiness().Excluir(id);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UsuarioLoginModel model)
        {
            using (SynsTicketContext context = new SynsTicketContext())
            {
                var usuario = context.UsuarioEntity.FirstOrDefault(u => u.Login == model.Login && u.Senha == model.Senha);
                if (usuario != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }


        public ActionResult UsuarioNomeUrl(string nome)
        {
            var dados = UsuarioBusiness.Pesquisar(nome);
            return Json(dados, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UsuarioIdUrl(int usuarioId)
        {
            var entity = UsuarioBusiness.Create(usuarioId);

            return Json(new { Nome = entity.Nome, UsuarioId = entity.UsuarioId }, JsonRequestBehavior.AllowGet);
        }


    }
}
