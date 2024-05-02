using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.IServices
{
    public interface IStudentService
    {
        public List<StudentResponseDTO> GetAllStudents();
        public List<StudentResponseDTO> GetPaginatedStudents(int currentPage, int itemPerPage);
        public StudentResponseDTO GetStudentById(int id);
        public void Create(StudentRequestDTO studentRequest);
        public void Update(int id,StudentRequestDTO studentRequest);
        public void Delete(int id);
        public Student GetStudentModelById(int id);
        public bool IsStudentExist(int id);
        public int GetTotalPages(int itemPerPage);
    }
}
