using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
    {
        builder
            .ToTable("IdentityUserLogin");        

        builder
            .HasKey(userLogin => new { userLogin.LoginProvider, userLogin.ProviderKey });

        builder
            .Property(userLogin => userLogin.LoginProvider)
            .HasMaxLength(128);

        builder
            .Property(userLogin => userLogin.ProviderKey)
            .HasMaxLength(128);

        builder
            .Property(userLogin => userLogin.ProviderDisplayName)
            .HasMaxLength(255);
    }
}
