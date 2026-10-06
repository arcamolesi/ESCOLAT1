using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESCOLAT1.Models;
using Microsoft.EntityFrameworkCore;
using ESCOLAT1.Models.ViewModel;

namespace ESCOLAT1.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) { }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Curso → Disciplina (1:N)
            modelBuilder
                .Entity<Disciplina>()
                .HasOne(d => d.Curso)
                .WithMany(c => c.Disciplinas)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata

            // Curso → Aluno (1:N)
            modelBuilder
                .Entity<Aluno>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Alunos)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Aluno → Nota (1:N) com Cascade
            modelBuilder
                .Entity<Nota>()
                .HasOne(n => n.Aluno)
                .WithMany(a => a.Notas)
                .HasForeignKey(n => n.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Disciplina → Nota (1:N) com Cascade
            modelBuilder
                .Entity<Nota>()
                .HasOne(n => n.Disciplina)
                .WithMany()
                .HasForeignKey(n => n.DisciplinaId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ESCOLAT1.Models.ViewModel.AgpNtPorCursoDisp> AgpNtPorCursoDisp { get; set; }
    }
}