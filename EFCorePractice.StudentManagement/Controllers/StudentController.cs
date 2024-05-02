using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Exceptions;
using EFCorePractice.StudentManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePractice.StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            return Ok(new ApiResponse() { Data = students, IsSuccess = true });
        }

        [HttpGet("paginate")]

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var student = _studentService.GetStudentById(id);
                return Ok(new ApiResponse() { Data=student, IsSuccess = true});
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
        public IActionResult CreateStudent(StudentRequestDTO studentRequest)
        {
            try
            {
                _studentService.Create(studentRequest);
                return Ok(new ApiResponse() { Data="Successfully Created",IsSuccess = true});
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
            catch (DataAlreadyExistsException alreadyExist)
            {
                return StatusCode(409, new ApiResponse() { Data = alreadyExist.Message, IsSuccess = false });
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

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentRequestDTO studentRequest)
        {
            try
            {
                _studentService.Update(id,studentRequest);
                return Ok(new ApiResponse() { Data = "Successfully Updated",IsSuccess=true});
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
            catch (DataAlreadyExistsException alreadyExist)
            {
                return StatusCode(409, new ApiResponse() { Data = alreadyExist.Message, IsSuccess = false });
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
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _studentService.Delete(id);
                return Ok(new ApiResponse() { Data="Successfully Deleted",IsSuccess = true});
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
