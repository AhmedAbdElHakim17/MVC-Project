namespace MVC_PROJECT.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }

        public List<Student> StdList { get; set; }
        public List<Instructor> InsList { get; set; }
    }
}
