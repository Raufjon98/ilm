using ilmV3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ilmV3.Infrastructure.Data.Configurations;
public class TeacherConfiguration : IEntityTypeConfiguration<TeacherEntity>
{
    public void Configure(EntityTypeBuilder<TeacherEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
