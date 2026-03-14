using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");
        
        builder.HasKey(Transaction => Transaction.Id);

        builder.Property(Transaction => Transaction.Title)
            .IsRequired(true)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(Transaction => Transaction.Type)
            .IsRequired(true)
            .HasColumnName("Type")
            .HasColumnType("SMALLINT");

        builder.Property(Transaction => Transaction.Amount)
            .IsRequired(true)
            .HasColumnName("Amount")
            .HasColumnType("MONEY");

        builder.Property(Transaction => Transaction.CreatedAt)
            .IsRequired(true)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME2");

        builder.Property(Transaction => Transaction.PaidOrReceivedAt)
            .IsRequired(false)
            .HasColumnName("PaidOrReceivedAt")
            .HasColumnType("DATETIME2");

        builder.Property(Transaction => Transaction.UserId)
            .IsRequired(true)
            .HasColumnName("UserId")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);            
    }
}

