using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.IRepository;
using EFCorePractice.StudentManagement.IServices;
using EFCorePractice.StudentManagement.Models;

namespace EFCorePractice.StudentManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        public CourseResponseDTO GetCourseById(int id)
        {
            if (!IsCourseExist(id))
            {
                throw new InvalidOperationException($"Course with {id} not found");
            }
            var course =  _courseRepository.GetCourseById(id);
            CourseResponseDTO responseCourse = new()
            {
                Id = course.Id,
                Name = course.Name
            };
            return responseCourse;
        }

        public bool IsCourseExist(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            return course is not null;
        }

        public void CreateBlog(CourseRequestDTO courseRequest)
        {
            if (courseRequest is null)
            {
                throw new ArgumentNullException(nameof(courseRequest), "Course cannot be null");
            }

            if (courseRequest.Name.Length < 2)
            {
                throw new ArgumentException("Course name must be at least 2 characters long.", nameof(courseRequest));
            }

            Course newCourse = new()
            {
                Name = courseRequest.Name,
            };

            var result = _courseRepository.CreateCourse(newCourse);
            if (!result)
            {
                throw new InvalidOperationException("Failed to create the course.");
            }
        }

        public void UpdateBlog(int id, CourseRequestDTO courseRequest)
        {
            if (courseRequest is null)
            {
                throw new ArgumentNullException(nameof(courseRequest), "Course cannot be null");
            }

            if (courseRequest.Name.Length < 2)
            {
                throw new ArgumentException("Course name must be at least 2 characters long.", nameof(courseRequest));
            }

            if (!IsCourseExist(id))
            {
                throw new InvalidOperationException($"Course with {id} not found");
            }

            Course updateCourse = new()
            {
                Id = id,
                Name = courseRequest.Name,
            };

            var result = _courseRepository.UpdateCourse(updateCourse);
            if (!result)
            {
                throw new InvalidOperationException("Failed to update the course.");
            }

        }

        public void DeleteBlog(int id)
        {
            if (!IsCourseExist(id))
            {
                throw new InvalidOperationException($"Course with {id} not found");
            }
            Course deleteCourse = new() { Id = id };
            var result = _courseRepository.DeleteCourse(deleteCourse);
            if (!result)
            {
                throw new InvalidOperationException("Failed to delete the course.");
            }
        }
    }
}
