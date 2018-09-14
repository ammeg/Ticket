using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.EntityFramework;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Ticket.ViewModels;
using Ticket.Models;

namespace Ticket.Controllers
{   
    public class DepartamentosController : Controller
    {	
		protected readonly ApplicationDbContext _db = new ApplicationDbContext();
        //
        // GET: /Departamentos/
        public async Task<ActionResult> Indice()
        {
            return await Pesquisa( new DepartamentosViewModel());
        }

		//
        // GET: /Departamentos/Pesquisa
		public async Task<ActionResult> Pesquisa(DepartamentosViewModel viewModel)
		{
			var query = _db.Departamentoes.AsQueryable();

			//TODO: parâmetros de pesquisa
			if (!String.IsNullOrWhiteSpace(viewModel.Descricao))
            {
                var descricaos = viewModel.Descricao?.Split(' ');
                query = query.Where(a => descricaos.All(descricao => a.Descricao.Contains(descricao)));
            }

            viewModel.Resultados = await query.OrderBy(a => a.Descricao).ToPagedListAsync(viewModel.Pagina, viewModel.TamanhoPagina);

            if (Request?.IsAjaxRequest() ?? false)
                return PartialView("_Pesquisa", viewModel);

            return View("Indice", viewModel);
		}

        //
        // GET: /Departamentos/Detalhes/5
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = await _db.Departamentoes.FirstOrDefaultAsync(a => a.DepartamentoId == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }  
            return View(departamento);
        }

        //
        // GET: /Departamentos/Criar        
		public ActionResult Criar()
        {
            return View();
        } 

        //
        // POST: /Departamentos/Criar
        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> Criar(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _db.Departamentoes.Add(departamento);
                await _db.SaveChangesAsync();
				TempData["Mensagem"] = "Operação realizada com sucesso!";
                return RedirectToAction("Indice");  
            }

            return View(departamento);
        }
        
        //
        // GET: /Departamentos/Editar/5 
        public async Task<ActionResult> Editar(int? id)
        {
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = await _db.Departamentoes.FirstOrDefaultAsync(a => a.DepartamentoId == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }            
            return View(departamento);
        }

        //
        // POST: /Departamentos/Editar/5
        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(departamento).State = EntityState.Modified;
                await _db.SaveChangesAsync();
				TempData["Mensagem"] = "Alteração realizada com sucesso!";
                return RedirectToAction("Indice");
            }

            return View(departamento);
        }

        //
        // GET: /Departamentos/Excluir/5 
        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = await _db.Departamentoes.FirstOrDefaultAsync(a => a.DepartamentoId == id);
            if (departamento == null)
            {
                return HttpNotFound();
            }   

  
            return View(departamento);
        }

        //
        // POST: /Departamentos/Excluir/5
        [HttpPost, ActionName("Excluir")]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmacao(int id)
        {
            Departamento departamento = await _db.Departamentoes.FirstOrDefaultAsync(a => a.DepartamentoId == id);
            _db.Departamentoes.Remove(departamento);
            await _db.SaveChangesAsync();
            return RedirectToAction("Indice");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
			{
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
