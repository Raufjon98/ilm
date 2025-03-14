using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
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
                  .  OnDelete(DeleteBehavior.NoAction); 
    }
}
