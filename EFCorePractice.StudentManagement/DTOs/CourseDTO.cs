namespace EFCorePractice.StudentManagement.DTOs
{
    public record CourseRequestDTO
    {
        public string Name { get; init; }
    }

    public class CourseResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
