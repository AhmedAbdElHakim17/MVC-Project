using MVC_PROJECT.CORE.Interfaces;

namespace MVC_PROJECT.Repositories
{
    public interface IStudentRepository: IBaseRepository<Student>
    {
        public List<CourseStd> SelectedIdsForCreation(List<int> ids);
        public List<CourseStd> SelectedIdsForShow(int id);
        public List<CourseStd> SelectedIdsForUpdate(List<int> ids, int id);
    }
}
