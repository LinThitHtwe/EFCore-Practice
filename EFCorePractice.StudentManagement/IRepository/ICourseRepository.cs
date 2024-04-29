using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.IRepository
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> GetAll();
        public IEnumerable<Course> GetPaginatedCourse(int currentPage,int itemPerPage);
        public Course GetCourseByName(string name);
        public int GetTotalPages(int itemPerPage);
        public Course GetCourseById(int id);
        public bool CreateCourse(Course course);
        public bool UpdateCourse(Course course);
        public bool DeleteCourse(Course course);

    }
}
