using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOLAT1.Models.ViewModel
{
    public class AgpNtPorCursoDisp
    {
        public int id { get; set; }
        public string curso { get; set; }
        public string disciplina { get; set; }  
        public float somaNota { get; set; } 
        public float contNota { get; set; } 
        public float medNota { get; set; }
        public float maxNota { get; set; }  
        public float minNota { get; set; }

    }
}