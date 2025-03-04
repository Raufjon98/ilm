using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ilmV3.Infrastructure.Data.Configurations;
public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(e => e.Id)
           .UseIdentityColumn()
           .ValueGeneratedOnAdd();

        builder.HasMany(s => s.Groups)
            .WithMany(e => e.Students)
           .UsingEntity(j => j
           .ToTable("StudentGroupMembership"));
    }
}
