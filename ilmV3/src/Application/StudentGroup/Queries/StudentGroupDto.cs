using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.StudentGroup.Queries;
public class StudentGroupDto
{
    public string? CodeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int SubjectId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudentGroupDto, StudentGroupEntity>();
        }
    }
}
