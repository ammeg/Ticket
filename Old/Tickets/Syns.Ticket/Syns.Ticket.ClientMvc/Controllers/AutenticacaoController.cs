using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Syns.Ticket.ClientMvc.Controllers
{
    public class AutenticacaoController : Controller
    {
        private IAutenticac aoProvider autenticacaoProvider;



        public AutenticacaoController(IAutenticacaoProvider autenticacaoProviderParam)
        {
            autenticacaoProvider = autenticacaoProviderParam;
        }



        public ActionResult Entrar()
        {
            ViewBag.Autenticado = autenticacaoProvider.Autenticado;

            return View(autenticacaoProvider.UsuarioAutenticado);
        }

        public ActionResult Sair()
        {
            autenticacaoProvider.Desautenticar();

            return RedirectToAction("Entrar");
        }


        [HttpPost]
        public ActionResult Entrar(AutenticacaoModel autenticacaoModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string msgErro;
                if (autenticacaoProvider.Autenticar(autenticacaoModel, out msgErro, "administrador"))
                {
                    return Redirect(returnUrl ?? Url.Action("Entrar", "Autenticacao"));
                }
                TempData["Mensagem"] = msgErro;
            }
            return RedirectToAction("Entrar");
        }

    }
}
