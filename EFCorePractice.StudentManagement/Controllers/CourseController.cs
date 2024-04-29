using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePractice.StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var courses = _courseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200,Type =typeof(CourseResponseDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetCourse(int id)
        {
            try
            {
                var course = _courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (InvalidOperationException ioe)
            {
                return NotFound($"{ioe.Message}");
            }
        }

    }
}
