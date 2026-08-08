using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
    {
        builder
            .ToTable("IdentityUserToken");

        builder
            .HasKey(userToken => new 
            { 
                userToken.UserId, 
                userToken.LoginProvider, 
                userToken.Name
            });

        builder
            .Property(userToken => userToken.LoginProvider)
            .HasMaxLength(120);

        builder
            .Property(userToken => userToken.Name)
            .HasMaxLength(180);
    }
}
