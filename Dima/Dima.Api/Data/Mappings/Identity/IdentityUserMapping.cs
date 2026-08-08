using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("IdentityUser");

        builder
            .HasKey(user => user.Id);

        builder
            .Property(user => user.Id)
            .IsRequired();

        builder
            .HasIndex(user => user.Email)
            .IsUnique();

        builder
            .HasIndex(user => user.NormalizedEmail)
            .IsUnique();

        builder
            .Property(user => user.UserName)
            .HasMaxLength(180);

        builder
            .Property(user => user.NormalizedUserName)
            .HasMaxLength(180);

        builder
            .Property(user => user.PhoneNumber)
            .HasMaxLength(20);

        builder
            .Property(user => user.ConcurrencyStamp)
            .IsConcurrencyToken();

        builder
            .HasMany<IdentityUserClaim<long>>()
            .WithOne()
            .HasForeignKey(userClaim => userClaim.UserId)
            .IsRequired();

        builder
            .HasMany<IdentityUserLogin<long>>()
            .WithOne()
            .HasForeignKey(userLogin => userLogin.UserId)
            .IsRequired();

        builder
            .HasMany<IdentityUserToken<long>>()
            .WithOne()
            .HasForeignKey(userToken => userToken.UserId)
            .IsRequired();

        builder
            .HasMany<IdentityUserRole<long>>()
            .WithOne()
            .HasForeignKey(userRole => userRole.UserId)
            .IsRequired();
    }
}
