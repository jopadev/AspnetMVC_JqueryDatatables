using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CRUD_EntityFramework.Models
{
    public class CondicaoLivroConfiguracao : EntityTypeConfiguration<CondicaoLivro>
    {
        public CondicaoLivroConfiguracao()
        {
            ToTable("CondicoesLivros");

            HasKey(x => x.CondicaoId);

            Property(x => x.CondicaoId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.Observacao).HasMaxLength(300).HasColumnType("varchar");
        }
    }
}