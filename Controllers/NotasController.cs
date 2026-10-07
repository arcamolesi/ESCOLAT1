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
    public class NotasController : Controller
    {
        private readonly Contexto _context;

        public NotasController(Contexto context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Notas.Include(n => n.Aluno).Include(n => n.Disciplina);
            return View(await contexto.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Disciplina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome");
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AlunoId,DisciplinaId,Semestre,Valor")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                //regra de negócio para somar a nota do aluno no campo valorHora 
                // regar ficticia criada para ilustrar alteração de dados em outra tabela, no caso a tabela Aluno
                var aluno = _context.Alunos.Find(nota.AlunoId);
                if (aluno != null)
                {
                    aluno.ValorHora = aluno.ValorHora + nota.Valor;
                }
                _context.Add(nota);
                await _context.SaveChangesAsync();
            }

            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", nota.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", nota.DisciplinaId);
            return View(nota);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", nota.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", nota.DisciplinaId);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AlunoId,DisciplinaId,Semestre,Valor")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", nota.AlunoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Descricao", nota.DisciplinaId);
            return View(nota);
        }

        // GET: Notas/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Aluno)
                .Include(n => n.Disciplina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                var aluno = _context.Alunos.Find(nota.AlunoId);
                if (aluno != null)
                {
                    aluno.ValorHora = aluno.ValorHora - nota.Valor;
                }

                _context.Notas.Remove(nota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }
    }
}
