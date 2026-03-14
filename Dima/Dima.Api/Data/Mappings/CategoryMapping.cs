using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(Category => Category.Id);

        builder.Property(Category => Category.Title)
            .IsRequired(true)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(Category => Category.Description)
            .IsRequired(false)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(Category => Category.UserId)
            .IsRequired(true)
            .HasColumnName("UserId")
            .HasColumnType("VARCHAR")            
            .HasMaxLength(160);
    }
}

