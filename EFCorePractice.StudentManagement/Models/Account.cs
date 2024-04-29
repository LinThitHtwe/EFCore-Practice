using System.ComponentModel.DataAnnotations;
using static EFCorePractice.StudentManagement.Enums.Enum;

namespace EFCorePractice.StudentManagement.Models
{
    public class Account
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
        public List<Student> Students { get; set; }
    }
}
