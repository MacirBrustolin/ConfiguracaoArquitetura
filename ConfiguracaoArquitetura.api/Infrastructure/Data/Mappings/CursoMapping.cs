﻿using ConfiguracaoArquitetura.api.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfiguracaoArquitetura.api.Infrastructure.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("TB_Curso");
            builder.HasKey(p => p.Codigo);
            builder.Property(p => p.Codigo).ValueGeneratedOnAdd();
            builder.Property(p => p.Nome);
            builder.Property(p => p.Descricao);
            builder.HasOne(p => p.Usuario).WithMany().HasForeignKey(fk => fk.CodigoUsuario);
        }
    }
}