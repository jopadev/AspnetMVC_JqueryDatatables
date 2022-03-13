using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_EntityFramework.Models
{
    public class Livro
    {
        public int LivroId { get; set; }
        public string Nome { get; set; }
        public string NumeroEdicao { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int NumeroExemplar { get; set; }
        public int CondicaoLivroId { get; set; }
        public virtual CondicaoLivro CondicaoLivro { get; set; }
        public bool DisponivelPraVenda { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Observacao { get; set; }
    }
}