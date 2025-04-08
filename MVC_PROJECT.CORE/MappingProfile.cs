using AutoMapper;
using MVC_PROJECT.CORE;
using MVC_PROJECT.Models;
namespace MVC_PROJECT
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.DeptName, src => src.MapFrom(src => src.Department != null? src.Department.Name : "No Department"))
                .ForMember(dest => dest.SelectedListIds, src => src.Ignore())
                .ForMember(dest => dest.Departments, src => src.Ignore())
                .ForMember(dest => dest.Courses, src => src.Ignore())
                .ReverseMap();
            //CreateMap<Department, DepartmentViewModel>()
                //.ForMember(dest=>dest.).ReverseMap();
            CreateMap<Instructor, InstructorViewModel>()
                .ForMember(dest => dest.DeptName, src => src.MapFrom(src => src.Department != null ? src.Department.Name : "No Department"))
                .ForMember(dest => dest.Departments, src => src.Ignore())
                .ForMember(dest => dest.CourseList, src => src.Ignore())
                .ReverseMap();
        }
    }
}
