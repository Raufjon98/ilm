using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ilmV3.Infrastructure.Data.Configurations;
public class TimeTableConfiguration : IEntityTypeConfiguration<TimeTableEntity>
{
    public void Configure(EntityTypeBuilder<TimeTableEntity> builder)
    {
        builder.HasKey(tt => tt.Id);

        builder.HasOne(t => t.Teacher)
     .WithMany(teacher => teacher.TimeTables)
     .HasForeignKey(t => t.TeacherId)
     .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.Subject)
            .WithMany(subject => subject.TimeTables)
            .HasForeignKey(t => t.SubjectId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.StudentGroup)
            .WithMany(group => group.TimeTables)
            .HasForeignKey(t => t.StudentGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
