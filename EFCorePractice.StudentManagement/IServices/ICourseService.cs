using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.IServices
{
    public interface ICourseService
    {
        public IEnumerable<Course> GetAllCourses();
        public CourseResponseDTO GetCourseById(int id);
        public bool IsCourseExist(int id);
        public void CreateBlog(CourseRequestDTO course);
        public void UpdateBlog(int  id, CourseRequestDTO course);
        public void DeleteBlog(int id);
    }
}
