using static EFCorePractice.StudentManagement.Enums.Enum;

namespace EFCorePractice.StudentManagement.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public List<Course> Courses { get; set; }
    }
}
