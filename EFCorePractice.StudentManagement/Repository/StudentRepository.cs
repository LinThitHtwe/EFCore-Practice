using EFCorePractice.StudentManagement.Data;
using EFCorePractice.StudentManagement.IRepository;
using EFCorePractice.StudentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice.StudentManagement.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }
        
        public bool Create(Student student)
        {
            _context.Students.Add(student);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool Delete(Student student)
        {
            _context.Students.Remove(student);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.AsNoTracking()
                                    .Include(c=>c.Account)
                                    .Include(c=>c.Courses)
                                    .ToList();
        }

        public Student? GetById(int id)
        {
            return _context.Students.AsNoTracking()
                                    .Include(c => c.Account)
                                    .Include(c => c.Courses)
                                    .FirstOrDefault(c=>c.Id ==id);
        }

        public IEnumerable<Student> GetPaginatedStudents(int currentPage, int itemPerPage)
        {
            return _context.Students.AsNoTracking()
                                   .Skip((currentPage - 1) * itemPerPage)
                                   .Take(itemPerPage)
                                   .Include(c => c.Account)
                                   .Include(c => c.Courses)
                                   .ToList();
        }

        public int GetTotalPages(int itemPerPage)
        {
            var rowCount = _context.Students.AsNoTracking().Count();
            return (int)Math.Ceiling((double)rowCount / itemPerPage);
        }

        public bool Update(Student student)
        {
            _context.Students.Update(student);
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}
