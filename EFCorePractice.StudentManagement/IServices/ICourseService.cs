using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.IServices
{
    public interface ICourseService
    {
        public IEnumerable<CourseResponseDTO> GetAllCourses();
        public IEnumerable<CourseResponseDTO> GetPaginatedCourses(int currentPage, int itemPerPage);
        public CourseResponseDTO GetCourseByIdResponse(int id);
        public Course GetCourseModelById(int id);
        public bool IsCourseExist(int id);
        public void CreateCourse(CourseRequestDTO course);
        public void UpdateCourse(int  id, CourseRequestDTO course);
        public void DeleteCourse(int id);
        public int GetTotalPage(int itemPerPage);
    }
}
