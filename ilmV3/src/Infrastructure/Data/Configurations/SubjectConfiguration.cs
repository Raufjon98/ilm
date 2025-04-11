using ilmV3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ilmV3.Infrastructure.Data.Configurations;
public class SubjectConfiguration : IEntityTypeConfiguration<SubjectEntity>
{
    public void Configure(EntityTypeBuilder<SubjectEntity> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(sg => sg.Teacher)
                  .WithOne(s => s.Subject)
                  .HasForeignKey<SubjectEntity>(s => s.TeacherId)
                     .OnDelete(DeleteBehavior.Restrict);
    }
}
