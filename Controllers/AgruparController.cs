using ESCOLAT1.Data;
using ESCOLAT1.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ESCOLAT1.Controllers
{
    public class AgruparController : Controller
    {
        private readonly Contexto contexto;

        public AgruparController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult AgruparNotaPorCurso()
        {
            
            var lista = from item in contexto.Notas
                                             .Include(d => d.Disciplina)
                                             .ThenInclude(c =>c.Curso)
                                             //.OrderBy(c=>c.Disciplina.Curso.Descricao)
                                             .ToList()
                                    let curso = item.Disciplina.Curso.Descricao
                                    group item by new {curso}
                                    into grupo
                                         orderby grupo.Key.curso
                                    select new AgpNtPorCursoDisp
                                    {
                                        curso = grupo.Key.curso,
                                        somaNota = grupo.Sum(a => a.Valor)
                                    };

           return View(lista); 
        }

        public IActionResult AgruparNotaPorCursoDisc()
        {
            
            var lista = from item in contexto.Notas
                                             .Include(d => d.Disciplina)
                                             .ThenInclude(c =>c.Curso)
                                             //.Where(s => s.Semestre ==1 )
                                             .ToList()

                                    let curso = item.Disciplina.Curso.Descricao
                                    let discp = item.Disciplina.Descricao
                                    
                                    group item by new {curso, discp}
                                    into grupo
                                         orderby grupo.Key.curso, grupo.Key.discp

                                    select new AgpNtPorCursoDisp
                                    {
                                        curso = grupo.Key.curso,
                                        disciplina = grupo.Key.discp, 
                                        somaNota = grupo.Sum(a => a.Valor), 
                                        contNota = grupo.Count(), 
                                        medNota = grupo.Average(a => a.Valor),
                                        maxNota = grupo.Max(m => m.Valor),
                                        minNota = grupo.Min(m => m.Valor)
                                    };

           return View(lista); 
        }




    }
}