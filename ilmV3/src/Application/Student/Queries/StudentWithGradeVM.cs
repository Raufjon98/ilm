using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.Student.Queries;
public class StudentWithGradeVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Grade { get; set; }

    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<StudentEntity, StudentWithGradeVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Grade, opt => opt.MapFrom(src =>
            src.Grades != null && src.Grades.Any()
            ? src.Grades.OrderByDescending(g => g.Id).FirstOrDefault()!.Grade 
            : 0  
    ));
        }
    }
}
