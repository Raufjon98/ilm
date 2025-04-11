using ilmV3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ilmV3.Infrastructure.Data.Configurations;
public class AbsentConfiguration : IEntityTypeConfiguration<AbsentEntity>
{
    public void Configure(EntityTypeBuilder<AbsentEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Student)
            .WithMany(x => x.Absents)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
