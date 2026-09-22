using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESCOLAT1.Data;
using ESCOLAT1.Models;

namespace ESCOLAT1.Controllers
{
    public class DisciplinasController : Controller
    {
        private readonly Contexto _context;

        public DisciplinasController(Contexto context)
        {
            _context = context;
        }

        // GET: Disciplinas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Disciplinas.Include(d => d.Curso);
            return View(await contexto.ToListAsync());
        }

        // GET: Disciplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas
                .Include(d => d.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // GET: Disciplinas/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Area");
            return View();
        }

        // POST: Disciplinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,CursoId")] Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Area", disciplina.CursoId);
            return View(disciplina);
        }

        // GET: Disciplinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Area", disciplina.CursoId);
            return View(disciplina);
        }

        // POST: Disciplinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,CursoId")] Disciplina disciplina)
        {
            if (id != disciplina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplinaExists(disciplina.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Area", disciplina.CursoId);
            return View(disciplina);
        }

        // GET: Disciplinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas
                .Include(d => d.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina != null)
            {
                _context.Disciplinas.Remove(disciplina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplinaExists(int id)
        {
            return _context.Disciplinas.Any(e => e.Id == id);
        }
    }
}
