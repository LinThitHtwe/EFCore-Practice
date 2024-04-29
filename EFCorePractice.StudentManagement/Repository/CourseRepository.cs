using EFCorePractice.StudentManagement.Data;
using EFCorePractice.StudentManagement.IRepository;
using EFCorePractice.StudentManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice.StudentManagement.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.AsNoTracking().ToList();
        }

        public Course? GetCourseById(int id)
        {
            return _context.Courses.FirstOrDefault(course => course.Id == id);
        }

        public bool CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}
