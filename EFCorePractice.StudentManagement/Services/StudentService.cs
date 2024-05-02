using EFCorePractice.StudentManagement.DTOs;
using EFCorePractice.StudentManagement.Exceptions;
using EFCorePractice.StudentManagement.IRepository;
using EFCorePractice.StudentManagement.IServices;
using EFCorePractice.StudentManagement.Models;
using EFCorePractice.StudentManagement.Repository;
using System.Collections.Generic;
using System.Security.Principal;
using static EFCorePractice.StudentManagement.Enums.Enum;

namespace EFCorePractice.StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAccountService _accountService;
        private readonly ICourseService _courseService;

        public StudentService(IStudentRepository studentRepository, IAccountService accountService, ICourseService courseService)
        {
            _studentRepository = studentRepository;
            _accountService = accountService;
            _courseService = courseService;
        }

        public void Create(StudentRequestDTO studentRequest)
        {
            if (studentRequest is null)
            {
                throw new ArgumentNullException(nameof(studentRequest), "Student Data cannot be null");
            }

            if (studentRequest.Name.Length < 2)
            {
                throw new ArgumentException("Student name must be at least 2 characters long.", nameof(studentRequest));
            }

            var account = _accountService.GetAccountModelById(studentRequest.AccountId);
            List<Course> courses = studentRequest.CourseIds.Select(id => _courseService.GetCourseModelById(id)).ToList();

            Student newStudent = new()
            {
                Name = studentRequest.Name,
                DOB = studentRequest.DOB,
                Account = account,
                Courses = courses,
                Gender = Enum.TryParse(studentRequest.Gender, out Gender parsedGender) ? parsedGender : throw new ArgumentException("Invalid gender. Please provide a valid gender.", nameof(studentRequest.Gender))
            };

            var result = _studentRepository.Create(newStudent);
            if (!result)
            {
                throw new InvalidOperationException("Failed to create the student.");
            }

        }

        public void Delete(int id)
        {
            if (!IsStudentExist(id))
            {
                throw new NotFoundException($"Student with id - {id} not found");
            }

            var result = _studentRepository.Delete(GetStudentModelById(id));
            if (!result)
            {
                throw new InvalidOperationException("Failed to delete the account.");
            }
        }

        public List<StudentResponseDTO> GetAllStudents()
        {
            var originalStudentsList = _studentRepository.GetAll();

            return originalStudentsList.Select(student => new StudentResponseDTO
            {
                Id = student.Id,
                Name = student.Name,
                Gender = student.Gender.ToString(),
                Account = ChangeToAccountResponseDTO(student.Account),
                Courses = student.Courses.Select(ChangeToCourseResponseDTO).ToList()
            }).ToList();
        }

        public List<StudentResponseDTO> GetPaginatedStudents(int currentPage, int itemPerPage)
        {
            if (currentPage > _studentRepository.GetTotalPages(itemPerPage))
            {
                throw new ArgumentOutOfRangeException(nameof(currentPage), "Current page exceeds the total number of pages.");
            }
            var originalStudentsList = _studentRepository.GetPaginatedStudents(currentPage, itemPerPage);

            return originalStudentsList.Select(student => new StudentResponseDTO
            {
                Id = student.Id,
                Name = student.Name,
                Gender = student.Gender.ToString(),
                Account = ChangeToAccountResponseDTO(student.Account),
                Courses = student.Courses.Select(ChangeToCourseResponseDTO).ToList()
            }).ToList();

        }

        public StudentResponseDTO GetStudentById(int id)
        {
            var student = _studentRepository.GetById(id) ?? throw new NotFoundException($"Student with Id `{id}` not found");

            return new StudentResponseDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Gender = student.Gender.ToString(),
                Account = ChangeToAccountResponseDTO(student.Account),
                Courses = student.Courses.Select(ChangeToCourseResponseDTO).ToList()
            };
        }

        public Student GetStudentModelById(int id)
        {
            var student = _studentRepository.GetById(id) ?? throw new NotFoundException($"Student with Id `{id}` not found");
            return student;
        }

        public void Update(int id, StudentRequestDTO studentRequest)
        {
            if (studentRequest is null)
            {
                throw new ArgumentNullException(nameof(studentRequest), "Request cannot be null");
            }
            var student = _studentRepository.GetById(id) ?? throw new NotFoundException($"Student with id `{id}` not found");
            var account = _accountService.GetAccountModelById(studentRequest.AccountId);
            student.Name = studentRequest.Name;
            student.Gender = Enum.TryParse(studentRequest.Gender, out Gender parsedGender) ? parsedGender : throw new ArgumentException("Invalid gender. Please provide a valid gender.", nameof(studentRequest.Gender));
            student.DOB = studentRequest.DOB;
            student.Account = account;
            student.Courses = studentRequest.CourseIds.Select(id => _courseService.GetCourseModelById(id)).ToList();
            var result = _studentRepository.Update(student);
            if (!result)
            {
                throw new InvalidOperationException("Failed to update the account.");
            }
        }

        public bool IsStudentExist(int id)
        {
            var student = _studentRepository.GetById(id);
            return student is not null;
        }

        public int GetTotalPages(int itemPerPage)
        {
            return _studentRepository.GetTotalPages(itemPerPage);
        }

        private static CourseResponseDTO ChangeToCourseResponseDTO(Course course)
        {
            return new CourseResponseDTO()
            {
                Id = course.Id,
                Name = course.Name,
            };
        }

        private static AccountResponseDTO ChangeToAccountResponseDTO(Account account)
        {
            return new AccountResponseDTO()
            {
                Id = account.Id,
                Email = account.Email,
                AccountType = account.AccountType.ToString()
            };
        }
    }
}
