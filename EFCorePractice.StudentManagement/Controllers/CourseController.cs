using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Exceptions;
using EFCorePractice.StudentManagement.IServices;
using EFCorePractice.StudentManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<List<CourseResponseDTO>>))]
        public IActionResult GetAllCourses()
        {
            var courses = _courseService.GetAllCourses();
            return Ok(new ApiResponse() { Data = courses, IsSuccess = true });
        }

        [HttpGet("paginate")]
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<PaginationResponse>))]
        public IActionResult GetPaginatedCourses([FromQuery] int currentPage = 1, [FromQuery] int itemPerPage = 10)
        {
            try
            {
                var courses = _courseService.GetPaginatedCourses(currentPage, itemPerPage);
                var totalPage = _courseService.GetTotalPage(itemPerPage);
                return Ok(new ApiResponse() { Data = new PaginationResponse() { CurrentPageNo = currentPage,PaginatedData= courses ,TotalPage= totalPage }, IsSuccess = true });
            }
            catch (ArgumentOutOfRangeException aore)
            {
                return BadRequest(new ApiResponse() { Data = aore.Message, IsSuccess = false });
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse() { Data = "Something went wrong", IsSuccess = false });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<CourseResponseDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetCourse(int id)
        {
            try
            {
                var course = _courseService.GetCourseByIdResponse(id);
                return Ok(new ApiResponse() { Data = course, IsSuccess = true });
            }
            catch (NotFoundException notFound)
            {
                return NotFound(new ApiResponse() { Data = notFound.Message, IsSuccess = false });
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse() { Data = "Something went wrong", IsSuccess = false });
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<string>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(424)]
        [ProducesResponseType(500)]
        public IActionResult CreateCourse(CourseRequestDTO courseRequest)
        {
            try
            {
                _courseService.CreateCourse(courseRequest);
                return Ok(new ApiResponse() { Data = "Successfully Created", IsSuccess = true });
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(new ApiResponse() { Data = ane.Message, IsSuccess = false });
            }
            catch (ArgumentException ae)
            {
                return BadRequest(new ApiResponse() { Data = ae.Message, IsSuccess = false });
            }
            catch (InvalidOperationException ioe)
            {
                return StatusCode(424, new ApiResponse() { Data = ioe.Message, IsSuccess = false });
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse() { Data = "Something went wrong", IsSuccess = false });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<string>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(424)]
        [ProducesResponseType(500)]
        public IActionResult UpdateCourse(int id, CourseRequestDTO courseRequest)
        {
            try
            {
                _courseService.UpdateCourse(id, courseRequest);
                return Ok(new ApiResponse() { Data = "Successfully Updated", IsSuccess = true });
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(new ApiResponse() { Data = ane.Message, IsSuccess = false });
            }
            catch (ArgumentException ae)
            {
                return BadRequest(new ApiResponse() { Data = ae.Message, IsSuccess = false });
            }
            catch (NotFoundException notFound)
            {
                return NotFound((new ApiResponse() { Data = notFound.Message, IsSuccess = false }));
            }
            catch (InvalidOperationException ioe)
            {
                return StatusCode(424, (new ApiResponse() { Data = ioe.Message, IsSuccess = false }));
            }
            catch (Exception)
            {
                return StatusCode(500, (new ApiResponse() { Data = "Something went wrong", IsSuccess = false }));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<string>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCourse(int id)
        {
            try
            {
                _courseService.DeleteCourse(id);
                return Ok((new ApiResponse() { Data = "Successfully Deleted", IsSuccess = true }));
            }
            catch (NotFoundException notFound)
            {
                return NotFound((new ApiResponse() { Data = notFound.Message, IsSuccess = false }));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest((new ApiResponse() { Data = ioe.Message, IsSuccess = false }));
            }
            catch (Exception)
            {
                return StatusCode(500, (new ApiResponse() { Data = "Something went wrong", IsSuccess = false }));
            }
        }
    }
}

