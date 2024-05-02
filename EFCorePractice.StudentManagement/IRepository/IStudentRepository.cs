using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.IRepository
{
    public interface IStudentRepository
    {
        public IEnumerable<Student> GetAll();
        public IEnumerable<Student> GetPaginatedStudents(int currentPage, int itemPerPage);
        public Student? GetById(int id);
        public bool Create(Student student);
        public bool Update(Student student);
        public bool Delete(Student student);
        public int GetTotalPages(int itemPerPage);
    }
}
