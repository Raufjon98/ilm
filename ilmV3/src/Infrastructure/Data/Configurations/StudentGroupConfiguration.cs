using ilmV3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ilmV3.Infrastructure.Data.Configurations;
public class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroupEntity>
{
    public void Configure(EntityTypeBuilder<StudentGroupEntity> builder)
    {
        builder.HasKey(sg => sg.Id);

        builder.HasOne(sg => sg.Subject)
                  .WithOne(s => s.StudentGroup)
                  .HasForeignKey<StudentGroupEntity>(sg => sg.SubjectId)
                  .OnDelete(DeleteBehavior.NoAction);
        builder
             .HasOne(sg => sg.Teacher)
             .WithMany(t => t.StudentGroups)
             .HasForeignKey(sg => sg.TeacherId)
             .OnDelete(DeleteBehavior.NoAction);
    }
}
