using static EFCorePractice.StudentManagement.Enums.Enum;

namespace EFCorePractice.StudentManagement.DTOs
{
    public class AccountRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
    }

    public class AccountResponseDTO
    {
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
    }
}
