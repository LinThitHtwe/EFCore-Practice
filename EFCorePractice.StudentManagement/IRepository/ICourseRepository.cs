using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.IRepository
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> GetAll();
        public Course GetCourseById(int id);
        public bool CreateCourse(Course course);
        public bool UpdateCourse(Course course);
        public bool DeleteCourse(Course course);

    }
}
