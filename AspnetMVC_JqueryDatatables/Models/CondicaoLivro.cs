using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_EntityFramework.Models
{
    public class CondicaoLivro
    {
        public int CondicaoId { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
    }
}