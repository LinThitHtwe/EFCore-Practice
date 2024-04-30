using System.Runtime.Serialization;
using static EFCorePractice.StudentManagement.Enums.Enum;

namespace EFCorePractice.StudentManagement.DTOs
{

    [DataContract]
    public class AccountRequestDTO
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }
        [DataMember(Name = "accountType")]
        public string AccountType { get; set; }
    }

    public class AccountResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
    }
}
