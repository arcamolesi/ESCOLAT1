using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOLAT1.Models
{
    public class Nota
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }

        [Required]
        [ForeignKey("Aluno")]
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        [Required]
        [ForeignKey("Disciplina")]
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

        [Required]
        [Range(1, 2)]
        public int Semestre { get; set; }

        [Required]
        [Range(0, 10)]
        public float Valor { get; set; }
    }
}