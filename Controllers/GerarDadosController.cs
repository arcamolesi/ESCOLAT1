using ESCOLAT1.Data;
using ESCOLAT1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESCOLAT1.Controllers
{
    public class GerarDadosController : Controller
    {
        private readonly Contexto contexto;

        public GerarDadosController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult gerarCursos()
        {
            contexto.Database.ExecuteSqlRaw("delete from cursos");
             contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('cursos', RESEED, 0)");
            
            contexto.Cursos.Add(new Curso { Descricao = "Engenharia de Software", Sigla = "ES", Area = "Exatas", Mensalidade = 2000 });
            contexto.Cursos.Add(new Curso { Descricao = "Análise e Dev de Sist", Sigla = "ADS", Area = "Exatas", Mensalidade = 1800 });
            contexto.Cursos.Add(new Curso { Descricao = "Ciência da Computação", Sigla = "CC", Area = "Exatas", Mensalidade = 2000 });
            contexto.Cursos.Add(new Curso { Descricao = "Direito", Sigla = "DIR", Area = "Humanas", Mensalidade = 2200 });
            contexto.Cursos.Add(new Curso { Descricao = "Medicina", Sigla = "MED", Area = "Saúde", Mensalidade = 10000 });
            contexto.Cursos.Add(new Curso { Descricao = "Enfermagem", Sigla = "ENF", Area = "Saúde", Mensalidade = 3000 });
            contexto.Cursos.Add(new Curso { Descricao = "Administração", Sigla = "ADM", Area = "Humanas", Mensalidade = 2000 });
            contexto.Cursos.Add(new Curso { Descricao = "Arquitetura e Urbanismo", Sigla = "ARQ", Area = "Exatas", Mensalidade = 2200 });
            contexto.Cursos.Add(new Curso { Descricao = "Psicologia", Sigla = "PSI", Area = "Humanas", Mensalidade = 2400 });
            contexto.Cursos.Add(new Curso { Descricao = "Fisioterapia", Sigla = "FSO", Area = "Saúde", Mensalidade = 2600 });

            contexto.SaveChanges();

            var lista = contexto.Cursos.ToList();

            return View(lista);
        }


        public IActionResult GerarDisciplinas()
        {
            contexto.Database.ExecuteSqlRaw("delete from disciplinas");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('disciplinas', RESEED, 0)");

            var cursos = contexto.Cursos.ToList();
            var random = new Random();

            foreach (var curso in cursos)
            {
                // Define uma quantidade aleatória entre 8 e 10 (o limite superior do Next é exclusivo, logo 11)
                int quantidadeDisciplinas = random.Next(8, 11);

                for (int i = 1; i <= quantidadeDisciplinas; i++)
                {
                    Disciplina disciplina = new Disciplina();
                    disciplina.Descricao = $"Disciplina {i}";// "Disciplina " + i.toString(): 
                    disciplina.CursoId = curso.Id;

                    contexto.Disciplinas.Add(disciplina);
                }
            }

            contexto.SaveChanges();
            return View (contexto.Disciplinas.Include(c=>c.Curso).OrderBy(o=>o.Curso.Descricao).ThenBy(d=>d.Descricao).ToList()); 

           // return View(contexto.Disciplinas.Include(d => d.Curso).ToList().OrderBy(d => d.Curso.Descricao).ThenBy(d => d.Descricao));
        }

        public IActionResult GerarAlunos()
        {

            contexto.Database.ExecuteSqlRaw("delete from alunos");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('alunos', RESEED, 0)");

            string[] vNomeMas = { "Miguel", "Arthur", "Bernardo", "Heitor", "Davi", "Lorenzo", "Théo", "Pedro", "Gabriel", "Enzo", "Matheus", "Lucas", "Benjamin", "Nicolas", "Guilherme", "Rafael", "Joaquim", "Samuel", "Enzo Gabriel", "João Miguel", "Henrique", "Gustavo", "Murilo", "Pedro Henrique", "Pietro", "Lucca", "Felipe", "João Pedro", "Isaac", "Benício", "Daniel", "Anthony", "Leonardo", "Davi Lucca", "Bryan", "Eduardo", "João Lucas", "Victor", "João", "Cauã", "Antônio", "Vicente", "Caleb", "Gael", "Bento", "Caio", "Emanuel", "Vinícius", "João Guilherme", "Davi Lucas", "Noah", "João Gabriel", "João Victor", "Luiz Miguel", "Francisco", "Kaique", "Otávio", "Augusto", "Levi", "Yuri", "Enrico", "Thiago", "Ian", "Victor Hugo", "Thomas", "Henry", "Luiz Felipe", "Ryan", "Arthur Miguel", "Davi Luiz", "Nathan", "Pedro Lucas", "Davi Miguel", "Raul", "Pedro Miguel", "Luiz Henrique", "Luan", "Erick", "Martin", "Bruno", "Rodrigo", "Luiz Gustavo", "Arthur Gabriel", "Breno", "Kauê", "Enzo Miguel", "Fernando", "Arthur Henrique", "Luiz Otávio", "Carlos Eduardo", "Tomás", "Lucas Gabriel", "André", "José", "Yago", "Danilo", "Anthony Gabriel", "Ruan", "Miguel Henrique", "Oliver" };
            string[] vNomeFem = { "Alice", "Sophia", "Helena", "Valentina", "Laura", "Isabella", "Manuela", "Júlia", "Heloísa", "Luiza", "Maria Luiza", "Lorena", "Lívia", "Giovanna", "Maria Eduarda", "Beatriz", "Maria Clara", "Cecília", "Eloá", "Lara", "Maria Júlia", "Isadora", "Mariana", "Emanuelly", "Ana Júlia", "Ana Luiza", "Ana Clara", "Melissa", "Yasmin", "Maria Alice", "Isabelly", "Lavínia", "Esther", "Sarah", "Elisa", "Antonella", "Rafaela", "Maria Cecília", "Liz", "Marina", "Nicole", "Maitê", "Isis", "Alícia", "Luna", "Rebeca", "Agatha", "Letícia", "Maria-", "Gabriela", "Ana Laura", "Catarina", "Clara", "Ana Beatriz", "Vitória", "Olívia", "Maria Fernanda", "Emilly", "Maria Valentina", "Milena", "Maria Helena", "Bianca", "Larissa", "Mirella", "Maria Flor", "Allana", "Ana Sophia", "Clarice", "Pietra", "Maria Vitória", "Maya", "Laís", "Ayla", "Ana Lívia", "Eduarda", "Mariah", "Stella", "Ana", "Gabrielly", "Sophie", "Carolina", "Maria Laura", "Maria Heloísa", "Maria Sophia", "Fernanda", "Malu", "Analu", "Amanda", "Aurora", "Maria Isis", "Louise", "Heloise", "Ana Vitória", "Ana Cecília", "Ana Liz", "Joana", "Luana", "Antônia", "Isabel", "Bruna" };
            string[] vSobrenomes = { "Silva", "Santos", "Oliveira", "Souza", "Rodrigues", "Ferreira", "Almeida", "Costa", "Gomes", "Martins", "Araújo", "Barbosa", "Ribeiro", "Carvalho", "Lima", "Gonçalves", "Melo", "Pereira", "Lopes", "Soares", "Fernandes", "Vieira", "Rocha", "Dias", "Nunes", "Cavalcante", "Monteiro", "Teixeira", "Cardoso", "Moura", "Campos", "Moreira", "Freitas", "Fonseca", "Barros", "Cruz", "Mendes", "Azevedo", "Castro", "Farias", "Batista", "Duarte", "Cunha", "Vasconcelos", "Guimarães", "Tavares", "Rezende", "Coelho", "Siqueira", "Magalhães" };
            string[] vCidade = { "São Paulo", "Rio de Janeiro", "Belo Horizonte", "Salvador", "Fortaleza", "Curitiba", "Recife", "Porto Alegre", "Goiânia", "Manaus" };
            string[] vEstado = { "SP", "RJ", "MG", "BA", "CE", "PR", "PE", "RS", "GO", "AM" };

            for (int i = 0; i < 100; i++)
            {
                Random rand = new Random();
                Random random = new Random();

                DateTime inicio = new DateTime(1956, 1, 1);
                DateTime fim = new DateTime(2080, 12, 31); // Assumindo 2080 como o ano pretendido

                int range = (fim - inicio).Days;
                DateTime dataAleatoria = inicio.AddDays(random.Next(range));

                Aluno aluno = new Aluno();
                aluno.Nome = ((i % 2 == 0) ? vNomeMas[i / 2] : vNomeFem[i / 2]) + " " + vSobrenomes[rand.Next(0, vSobrenomes.Length)];
                aluno.Nascimento = dataAleatoria;
                aluno.ValorHora = (float)(rand.NextDouble() * 200); // Valor aleatório entre 0 e 200   
                aluno.CursoId = rand.Next(1, 10);

                contexto.Alunos.Add(aluno);
            }

            contexto.SaveChanges();
            return View(contexto.Alunos.Include(a => a.Curso).ToList().OrderBy(a => a.Nome));
        }


        public IActionResult GerarNotas()
        {
            // Carrega todos os alunos com seus respectivos cursos e as disciplinas do curso
            var alunos = contexto.Alunos
                .Include(a => a.Curso)
                    .ThenInclude(c => c.Disciplinas)
                .ToList();

            var random = new Random();

            foreach (var aluno in alunos)
            {
                // Verifica se o aluno possui um curso válido com disciplinas cadastradas
                if (aluno.Curso?.Disciplinas == null || !aluno.Curso.Disciplinas.Any())
                    continue;

                foreach (var disciplina in aluno.Curso.Disciplinas)
                {


                    // Adiciona a nota do primeiro semestre
                    contexto.Notas.Add(new Nota
                    {
                        AlunoId = aluno.Id,
                        DisciplinaId = disciplina.Id,
                        Semestre = 1,
                        Valor = (float)Math.Round((decimal)(random.NextDouble() * 10), 2)
                    });

                    // Adiciona a nota do segundo semestre
                    contexto.Notas.Add(new Nota
                    {
                        AlunoId = aluno.Id,
                        DisciplinaId = disciplina.Id,
                        Semestre = 2,
                        Valor = (float)Math.Round((decimal)(random.NextDouble() * 10), 2)
                    });
                }
            }


            contexto.SaveChanges();

            return View(contexto.Notas.Include(n => n.Aluno).Include(n => n.Disciplina).ToList().OrderBy(n => n.Aluno.Nome).ThenBy(n => n.Disciplina.Descricao));

        }


    }
}
