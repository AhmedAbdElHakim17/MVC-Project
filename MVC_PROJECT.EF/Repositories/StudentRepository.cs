using MVC_PROJECT.EF.Repositories;
namespace MVC_PROJECT.Repositories
{
    public class StudentRepository: BaseRepository<Student>, IStudentRepository
    {
        private readonly AppDbContext context;

        public StudentRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public List<CourseStd> SelectedIdsForCreation(List<int> ids)
        {
            var courses = ids.Select(cId => new CourseStd { CrsId = cId }).ToList();
            return courses;
        }
        public List<CourseStd> SelectedIdsForUpdate(List<int> ids, int id)
        {
            //var CrsStdList = context.courseStds.Where(cs => cs.StdId == id).ToList();
            //var remList = CrsStdList.Where(cs => !ids.Contains(cs.CrsId)).ToList();
            //context.courseStds.RemoveRange(remList);
            //var CrsStdListIds = CrsStdList.Select(cs=>cs.CrsId).ToList();
            //var coursesToAdd = ids.Where(cId => !CrsStdListIds.Contains(cId))
            //    .Select(cId => new CourseStd { CrsId = cId }).ToList();
            //return coursesToAdd;
            context.courseStds.Where(cs => cs.StdId == id).ExecuteDelete();
            var NewList = ids.Select(id => new CourseStd {CrsId = id}).ToList();
            return NewList;
        }
        public List<CourseStd> SelectedIdsForShow(int id)
        {
            var courses = context.courseStds.Include(cs=>cs.Course).Where(cs=> cs.StdId == id).ToList();
            return courses;
        }

        public Student GetByEmail(string email)
        {
            Student student = context.students.Include(s => s.Department).Include(s => s.CourseStds).ThenInclude(cs => cs.Course).FirstOrDefault(x => x.Email == email);
            return student;
        }
    }
}
