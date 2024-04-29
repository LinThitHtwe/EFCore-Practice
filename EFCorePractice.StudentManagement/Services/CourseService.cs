using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Exceptions;
using EFCorePractice.StudentManagement.IRepository;
using EFCorePractice.StudentManagement.IServices;
using EFCorePractice.StudentManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EFCorePractice.StudentManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<CourseResponseDTO> GetAllCourses()
        {
            var courses = _courseRepository.GetAll();
            List<CourseResponseDTO> responseLists = new();
            foreach (var course in courses)
            {
                responseLists.Add(new CourseResponseDTO() { Id = course.Id, Name = course.Name });
            };
            return responseLists;
        }

        public IEnumerable<CourseResponseDTO> GetPaginatedCourses(int currentPage, int itemPerPage)
        {
            if (currentPage > _courseRepository.GetTotalPages(itemPerPage))
            {
                throw new ArgumentOutOfRangeException(nameof(currentPage), "Current page exceeds the total number of pages.");
            }
            var courses = _courseRepository.GetPaginatedCourse(currentPage, itemPerPage);
            List<CourseResponseDTO> responseLists = new();
            foreach (var course in courses)
            {
                responseLists.Add(new CourseResponseDTO() { Id = course.Id, Name = course.Name });
            }
            return responseLists;
        }

        public CourseResponseDTO GetCourseByIdResponse(int id)
        {
            if (!IsCourseExist(id))
            {
                throw new NotFoundException($"Course with {id} not found");
            }
            var course = _courseRepository.GetCourseById(id);
            CourseResponseDTO responseCourse = new()
            {
                Id = course.Id,
                Name = course.Name
            };
            return responseCourse;
        }

        public Course GetCourseModelById(int id)
        {
            if (!IsCourseExist(id))
            {
                throw new NotFoundException($"Course with {id} not found");
            }
            return _courseRepository.GetCourseById(id);
        }

        public bool IsCourseExist(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            return course is not null;
        }

        public void CreateCourse(CourseRequestDTO courseRequest)
        {
            if (courseRequest is null)
            {
                throw new ArgumentNullException(nameof(courseRequest), "Course cannot be null");
            }

            if (courseRequest.Name.Length < 2)
            {
                throw new ArgumentException("Course name must be at least 2 characters long.", nameof(courseRequest));
            }
            if(_courseRepository.GetCourseByName(courseRequest.Name) is not null)
            {
                throw new DataAlreadyExistsException($"Course name `${courseRequest.Name}` already exists");
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

        public void UpdateCourse(int id, CourseRequestDTO courseRequest)
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
                throw new NotFoundException($"Course with {id} not found");
            }

            var isCourseWithSameName = _courseRepository.GetCourseByName(courseRequest.Name);

            if (isCourseWithSameName is not null && isCourseWithSameName.Id != id)
            {
                throw new DataAlreadyExistsException($"Course name `${courseRequest.Name}` already exists");
            }

            var course = GetCourseModelById(id);
            course.Name = courseRequest.Name;

            var result = _courseRepository.UpdateCourse(course);
            if (!result)
            {
                throw new InvalidOperationException("Failed to update the course.");
            }

        }

        public void DeleteCourse(int id)
        {
            if (!IsCourseExist(id))
            {
                throw new NotFoundException($"Course with {id} not found");
            }
            var result = _courseRepository.DeleteCourse(GetCourseModelById(id));
            if (!result)
            {
                throw new InvalidOperationException("Failed to delete the course.");
            }
        }

        public int GetTotalPage(int itemPerPage)
        {
            return _courseRepository.GetTotalPages(itemPerPage);
        }
        
    }
}
