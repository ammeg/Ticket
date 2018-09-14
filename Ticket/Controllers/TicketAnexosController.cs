using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using PagedList.EntityFramework;
using System.Net;
using Ticket.ViewModels;
using Ticket.Models;

namespace Ticket.Controllers
{   

    public class TicketAnexosController : Controller
    {		
		protected readonly ApplicationDbContext _db = new ApplicationDbContext();
        //
        // GET: /TicketAnexos/IndicePartial
        public async Task<ActionResult> IndicePartial(TicketAnexosPartialViewModel viewModel)
        {
			if (viewModel.Pesquisa == null)
                viewModel.Pesquisa = new TicketAnexosViewModel();

            var query = _db.TicketAnexoes.Where(a => a.TicketId == viewModel.TicketId);

			viewModel.Pesquisa.Resultados = await query.ToPagedListAsync(viewModel.Pesquisa.Pagina, viewModel.Pesquisa.TamanhoPagina);			

            return PartialView("_Indice", viewModel);
        }
		
        //
        // GET: /TicketAnexos/CriarPartial
		public ActionResult CriarPartial(int ticketId)
        {
            return PartialView("_Criar", new TicketAnexo() { TicketId = ticketId });
        } 

        //
        // POST: /TicketAnexos/CriarPartial
        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> CriarPartial([Bind(Include = "")]TicketAnexo ticketAnexo)
        {
            if (ModelState.IsValid)
            {
                _db.TicketAnexoes.Add(ticketAnexo);
                await _db.SaveChangesAsync();
                
				return await IndicePartial(new TicketAnexosPartialViewModel() { TicketId = ticketAnexo.TicketId });
            }

            return PartialView("_Criar", ticketAnexo);
        }
        
        //
        // GET: /TicketAnexos/EditarPartial/5 
        public async Task<ActionResult> EditarPartial(int? id)
        {
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAnexo ticketAnexo = await _db.TicketAnexoes.FindAsync(id);
            if (ticketAnexo == null)
            {
                return HttpNotFound();
            }            

            return PartialView("_Editar", ticketAnexo);
        }

        //
        // POST: /TicketAnexos/EditarPartial/5
        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarPartial([Bind(Include = "")]TicketAnexo ticketAnexo)
        {
			if (ModelState.IsValid)
            {
                _db.Entry(ticketAnexo).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                
				return await IndicePartial(new TicketAnexosPartialViewModel() { TicketId = ticketAnexo.TicketId });
            }

            return PartialView("_Editar", ticketAnexo);
        }

        //
        // GET: /TicketAnexos/ExcluirPartial/5 
        public async Task<ActionResult> ExcluirPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAnexo ticketAnexo = await _db.TicketAnexoes.FindAsync(id);
            if (ticketAnexo == null)
            {
                return HttpNotFound();
            }   
  
            return PartialView("_Excluir", ticketAnexo);
        }

        //
        // POST: /TicketAnexos/ExcluirPartial/5
        [HttpPost, ActionName("ExcluirPartial")]
		[ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmacaoPartial(int id)
        {
            TicketAnexo ticketAnexo = await _db.TicketAnexoes.FindAsync(id);
            _db.TicketAnexoes.Remove(ticketAnexo);
            await _db.SaveChangesAsync();
            
			return await IndicePartial(new TicketAnexosPartialViewModel() { TicketId = ticketAnexo.TicketId });
        }

		public async Task<ActionResult> CancelarPartial(int ticketId)
        {
            return await IndicePartial(new TicketAnexosPartialViewModel() { TicketId = ticketId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
