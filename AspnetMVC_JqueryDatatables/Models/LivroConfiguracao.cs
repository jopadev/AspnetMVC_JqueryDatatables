using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CRUD_EntityFramework.Models
{
    public class LivroConfiguracao : EntityTypeConfiguration<Livro>
    {
        public LivroConfiguracao()
        {
            ToTable("Livros");

            HasKey(x => x.LivroId);

            Property(x => x.LivroId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.Nome).HasMaxLength(120).HasColumnType("varchar");

            Property(x => x.Editora).HasMaxLength(120).HasColumnType("varchar");

            Property(x => x.Autor).HasMaxLength(120).HasColumnType("varchar");

            Property(x => x.NumeroEdicao).HasMaxLength(10).HasColumnType("char");

            HasRequired(x => x.CondicaoLivro).WithMany().HasForeignKey(y => y.CondicaoLivroId);

            Property(x => x.PrecoVenda).HasPrecision(10, 2);

            Property(x => x.Observacao).HasMaxLength(300).HasColumnType("varchar");
        }
    }
}