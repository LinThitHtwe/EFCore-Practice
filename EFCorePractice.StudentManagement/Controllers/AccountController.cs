using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Exceptions;
using EFCorePractice.StudentManagement.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePractice.StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<List<AccountResponseDTO>>))]
        public IActionResult GetAllAccount()
        {
            var accounts = _accountService.GetAll();
            return Ok(new ApiResponse() { Data = accounts, IsSuccess = true });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<AccountResponseDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetAccountById(int id)
        {
            try
            {
                var account = _accountService.GetById(id);
                return Ok(new ApiResponse() { Data = account, IsSuccess = true });
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

        [HttpGet("paginate")]
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<List<AccountResponseDTO>>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetPaginatedAccounts([FromQuery] int currentPage = 1, [FromQuery] int itemPerPage = 10)
        {
            try
            {
                var accounts = _accountService.GetPaginatedAccounts(currentPage, itemPerPage);
                var totalPages = _accountService.GetTotalPages(itemPerPage);
                return Ok(new ApiResponse() { Data = new PaginationResponse() { CurrentPageNo = currentPage, PaginatedData = accounts, TotalPage = totalPages }, IsSuccess = true });
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

        [HttpPost]
        [ProducesResponseType(200,Type = typeof(ApiResponseSuccess<string>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(424)]
        [ProducesResponseType(500)]
        public IActionResult CreateAccount(AccountRequestDTO accountRequest)
        {
            try
            {
                _accountService.Create(accountRequest);
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
            catch (DataAlreadyExistsException alreadyExist)
            {
                return StatusCode(409, new ApiResponse() { Data = alreadyExist.Message, IsSuccess = false });
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
        public IActionResult UpdateAccount(int id,AccountRequestDTO accountRequest)
        {
            try
            {
                _accountService.Update(id, accountRequest);
                return Ok(new ApiResponse() { Data="Successfully Updated",IsSuccess=true});
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
        [ProducesResponseType(200, Type = typeof(ApiResponseSuccess<string>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteAccount(int id)
        {
            try
            {
                _accountService.Delete(id);
                return Ok(new ApiResponse() { Data="Successfully Deleted",IsSuccess =true});
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
