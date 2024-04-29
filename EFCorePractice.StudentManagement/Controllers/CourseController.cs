using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Exceptions;
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
        [ProducesResponseType(200,Type =typeof(List<CourseResponseDTO>))]
        public IActionResult GetAllCourses()
        {
            var courses = _courseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CourseResponseDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetCourse(int id)
        {
            try
            {
                var course = _courseService.GetCourseByIdResponse(id);
                return Ok(course);
            }
            catch (NotFoundException notFound)
            {
                return NotFound($"{notFound.Message}");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(424)]
        [ProducesResponseType(500)]
        public IActionResult CreateCourse(CourseRequestDTO courseRequest)
        {
            try
            {
                _courseService.CreateCourse(courseRequest);
                return Ok("Successfully Created");
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(ane.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return StatusCode(424, ioe.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(424)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCourse(int id, CourseRequestDTO courseRequest)
        {
            try
            {
                _courseService.UpdateCourse(id, courseRequest);
                return Ok("Successfully Updated");
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(ane.Message);
            }
            catch (NotFoundException notFound)
            {
                return NotFound(notFound.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return StatusCode(424, ioe.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCourse(int id)
        {
            try
            {
                _courseService.DeleteCourse(id);
                return Ok("Successfully Deleted");
            }
            catch (NotFoundException notFound)
            {
                return NotFound($"{notFound.Message}");
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
