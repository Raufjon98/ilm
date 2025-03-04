using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.TodoLists.Queries.GetTodos;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.Student.Queries;
public class StudentDto
{
    public string Name { get; set; } = string.Empty;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudentDto, StudentEntity>();
        }
    }
}
