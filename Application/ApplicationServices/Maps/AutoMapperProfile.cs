using AutoMapper;
using Domain.Entities;

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
