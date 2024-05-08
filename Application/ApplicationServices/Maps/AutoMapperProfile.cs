using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationServices.Maps
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapForStudent();
        }

        public void MapForStudent()
        {
            CreateMap<StudentDto, Student>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Student, StudentDto>();
        }

    }
}
