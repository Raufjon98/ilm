using ilmV3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ilmV3.Infrastructure.Data.Configurations;
public class GradeConfiguration : IEntityTypeConfiguration<GradeEntity>
{
    public void Configure(EntityTypeBuilder<GradeEntity> builder)
    {
        builder.HasKey(g=>g.Id);
    
        builder.HasOne(g => g.Student)
            .WithMany(x=>x.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
            
          
    }
}
