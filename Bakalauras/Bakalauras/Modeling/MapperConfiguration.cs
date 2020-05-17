using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Modeling.DTO;
using Bakalauras.Modeling.Models;

namespace Bakalauras.Modeling
{
    public class MapperConfiguration : AutoMapper.Profile
    {
        public MapperConfiguration()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<Class, ClassDTO>().ReverseMap();
            CreateMap<Visualization, VisualizationDTO>().ReverseMap();
        }
    }
}
