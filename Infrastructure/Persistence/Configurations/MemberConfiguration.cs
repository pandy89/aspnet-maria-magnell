using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<MemberEntity>
{
    public void Configure(EntityTypeBuilder<MemberEntity> builder)
    {
        //builder.HasKey(x => x.Id)
        //    .HasName("PK_Members_Id");

        //builder.Property(x => x.FirstName)
        //    .HasMaxLength(100);

        //builder.Property(x => x.LastName)
        //    .HasMaxLength(100);

        //builder.Property(x => x.PhoneNumber);

        //builder.Property(x => x.ProfileImageUrl);

        //builder.Property(x => x.CreatedAt)
        //    .HasColumnType("datetime2(0)")
        //    .HasDefaultValueSql("(SYSUTCDATETIME())", "DF_Members_CreatedAt")
        //    .ValueGeneratedOnAdd();

        //builder.Property(x => x.ModifiedAt)
        //    .HasColumnType("datetime2(0)")
        //    .HasDefaultValueSql("(SYSUTCDATETIME())", "DF_Members_ModifiedAt")
        //    .ValueGeneratedOnAddOrUpdate();

        //builder.Property(x => x.IsDeleted)
        //    .HasDefaultValue(false);

        //builder.Property(x => x.RowVersion)
        //    .IsRowVersion();
    }
}
