using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<MemberEntity>
{
    public void Configure(EntityTypeBuilder<MemberEntity> builder)
    {
        builder.HasKey(x => x.Id)
            .HasName("PK_Members_Id");

        builder.Property(x => x.FirstName)
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .HasMaxLength(100);

        builder.Property(x => x.PhoneNumber);

        builder.Property(x => x.ProfileImageUrl);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ModifiedAt);

        builder.Property(x => x.IsDeleted);

        builder.Property(x => x.RowVersion)
            .IsRowVersion();
            

        builder
            .HasOne<ApplicationUser>() // Koppling till ApplicationUser
            .WithOne() // Koppling mellan ApplicationUser och MemberEntity
            .HasForeignKey<MemberEntity>(x => x.Id)
            .HasPrincipalKey<ApplicationUser>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // Om användarens tas bort så tas även medlem bort.
    }
}
