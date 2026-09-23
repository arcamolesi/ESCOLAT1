using ESCOLAT1.Data;
using ESCOLAT1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ESCOLAT1.Controllers
{
    public class FiltrarController : Controller
    {
        private readonly Contexto contexto;

        public FiltrarController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult ListarNotas()
        {
            var lista = contexto.Notas
            .Include(a => a.Aluno)
            .Include(d => d.Disciplina)
            .ThenInclude(c => c.Curso)
            .OrderBy(c=>c.Aluno.Curso.Descricao)
            .ThenBy(a => a.Aluno.Nome)
            .ThenBy(d=>d.Disciplina)
            .ToList(); 

            return View(lista); 
        }

        [HttpGet]
        public IActionResult FiltrarAlunoNota()
        {
            ViewData["AlunoId"] = new SelectList(contexto.Alunos, "Id", "Nome");
            ViewData["DisciplinaId"] = new SelectList(contexto.Disciplinas, "Id", "Descricao");
            return View();
        }


    [HttpPost]
    public IActionResult FiltrarAlunoNota(int? alunoId, int? disciplinaId)
    {
          
      var notas = contexto.Notas
                .Include(a => a.Aluno)
                .Include(d => d.Disciplina)
                .ThenInclude(d => d.Curso)
                .Where(n => !alunoId.HasValue || n.AlunoId == alunoId.Value)
                .ToList();
        
            return View("listarNotas", notas);
        }
        

    }
}
