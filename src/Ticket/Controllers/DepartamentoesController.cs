using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.Models;

namespace Ticket.Controllers
{
    public class DepartamentoesController : Controller
    {
        private readonly TicketContext _context;

        public DepartamentoesController(TicketContext context)
        {
            _context = context;    
        }

        // GET: Departamentoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamento.ToListAsync());
        }

        // GET: Departamentoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.SingleOrDefaultAsync(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentoId,DataHoraAlteracao,DataHoraCad,DepartamentoMasterId,Descricao,Email,Sigla,UsuarioAlteracao,UsuarioCad")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                departamento.DepartamentoId = Guid.NewGuid();
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        // GET: Departamentoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.SingleOrDefaultAsync(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DepartamentoId,DataHoraAlteracao,DataHoraCad,DepartamentoMasterId,Descricao,Email,Sigla,UsuarioAlteracao,UsuarioCad")] Departamento departamento)
        {
            if (id != departamento.DepartamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        // GET: Departamentoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamento.SingleOrDefaultAsync(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var departamento = await _context.Departamento.SingleOrDefaultAsync(m => m.DepartamentoId == id);
            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DepartamentoExists(Guid id)
        {
            return _context.Departamento.Any(e => e.DepartamentoId == id);
        }
    }
}
