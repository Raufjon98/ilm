﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.Teacher.Queries;
public class TeacherDto
{
    public string Name { get; set; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TeacherDto, TeacherEntity>();
        }
    }
}
