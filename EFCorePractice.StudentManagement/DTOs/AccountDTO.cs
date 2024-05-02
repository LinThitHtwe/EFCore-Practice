using System.Runtime.Serialization;
using static EFCorePractice.StudentManagement.Enums.Enum;

namespace EFCorePractice.StudentManagement.DTOs
{

    public record AccountRequestDTO
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string AccountType { get; init; }
    }

    public class AccountResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
    }
}
