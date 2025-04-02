using AutoMapper;
namespace MVC_PROJECT
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Student, StudentViewModel>();
        }  
    }
}
