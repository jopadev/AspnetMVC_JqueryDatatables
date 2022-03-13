using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD_EntityFramework.Models
{
    public class MeuContexto : DbContext
    {
        public MeuContexto() : base("DefaultConnection")
        {

        }

        public DbSet<CondicaoLivro> CondicaoLivros { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CondicaoLivroConfiguracao());
            modelBuilder.Configurations.Add(new LivroConfiguracao());

            base.OnModelCreating(modelBuilder);
        }
    }
}