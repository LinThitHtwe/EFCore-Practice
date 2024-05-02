using EFCorePractice.StudentManagement.Models;
using static EFCorePractice.StudentManagement.Enums.Enum;

namespace EFCorePractice.StudentManagement.DTOs
{
    public record StudentRequestDTO
    {
        public string Name { get; init; }
        public DateTime DOB { get; init; }
        public string Gender { get; init; }
        public int AccountId { get; init; }
        // public int Account { get; init; }
        public List<int> CourseIds { get; init; }
    }

    public class StudentResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public AccountResponseDTO Account { get; set; }
        public List<CourseResponseDTO> Courses { get; set; }

    }
}
